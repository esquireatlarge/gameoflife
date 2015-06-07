//NEEDED:
//Implement buy system

#region File Description
//This is a modified version of one of Microsoft's templates.
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace GoL
{
    /// <summary>
    /// This screen implements the actual game logic.
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont;
        SpriteFont hudFont;
        Random random = new Random();
        Board b;
        Hud hud;
        Texture2D h_panel;
        Camera cam;
        Texture2D board;
        const int MAX_PLAYERS = 8;
        const int MAX_TILES = 147;
        const int MAX_STOCK = 10;
        const int MAX_CAREER = 7;
        int n_players;
        Player[] plArray;
        Vector2 imagePosition;
        Tile[] tiles;
        Stock[] stocks;
        CareerCard[] careers;
        //The two below are used for the event handlers, because as far as I have learned,
        //they can't be passed as parameters...
        int tempPlayerIndex;
        int tileSplitIndex;
        int currPlayer = 0;
        int largestSpin = 0;
        bool firstHasBeenDetermined = false;
        int stopSignLocation;
        SelectLeftRightScreen slr = new SelectLeftRightScreen("Go left or right?");
        bool hasSpun;
        VictoryScreen vs;
        Bank bank;
        BuyScreen bs = new BuyScreen("Buy stock or insurance!");

        #region Camera - Related
        private const float zoomRate = 0.7f;
        private const float movementRate = 500f;
        private const float rotationRate = 1.0f;
        #endregion
        #endregion

        #region Properties
        public Stock[] StockArray
        {
            get
            {
                return stocks;
            }
        }
        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            //These are for the transition.
            TransitioningOnTime = TimeSpan.FromSeconds(1.5);
            TransitioningOffTime = TimeSpan.FromSeconds(0.5);
            n_players = 2;
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            board = content.Load<Texture2D>("board");
            h_panel = content.Load<Texture2D>("hudpanel");
            //The board has 147 tiles.
            b = new Board(ScreenManager.GraphicsDevice, board, 147);
            b.PlayerCount = n_players;
            cam = new Camera();
            hudFont = content.Load<SpriteFont>("hudfont");
            hud = new Hud(ScreenManager.GraphicsDevice, h_panel, hudFont);
            tiles = new Tile[MAX_TILES];
            stocks = new Stock[MAX_STOCK];
            careers = new CareerCard[MAX_CAREER];
            bank = new Bank();
            initializeTiles();
            initializeCareers();

            //Make the players.
            plArray = new Player[8];
            for (int i = 0; i < n_players; i++)
            {
                plArray[i] = new Player(content.Load<Texture2D>("testplayerdot"), ScreenManager.GraphicsDevice);
                plArray[i].Position = new Vector2(1713.0f, 640.0f) ;
                //0 is default color, which is a blue of some kind.
                if (i == 1)
                {
                    plArray[i].ChosenColor = Color.DimGray;
                }
                if (i == 2)
                {
                    plArray[i].ChosenColor = Color.Firebrick;
                }
                if (i == 3)
                {
                    plArray[i].ChosenColor = Color.RoyalBlue;
                }
                if (i == 4)
                {
                    plArray[i].ChosenColor = Color.Orange;
                }
                if (i == 5)
                {
                    plArray[i].ChosenColor = Color.Chartreuse;
                }
                if (i == 6)
                {
                    plArray[i].ChosenColor = Color.DeepPink;
                }
                if (i == 7)
                {
                    plArray[i].ChosenColor = Color.Tan;
                }
            }

            //Load the stock.
            for (int i = 0; i < MAX_STOCK; i++)
            {
                stocks[i] = new Stock(i+1);
            }

            gameFont = content.Load<SpriteFont>("gamefont");

            slr.LeftSelected += ChoseLeftPath;
            slr.RightSelected += ChoseRightPath;
            bs.BuyStock += StockBought;
            bs.BuyInsurance += InsuranceBought;

            //Sleep for a while so we get a look at the loading screen.
            //Changing the 1000 value changes how long the loading screen
            //lasts on the page.
            Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();

            //Reset camera position.
            resetPosition();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            //If the camera doesn't move, we don't have to draw the screen,
            //so set the camera to Unchanged (meaning it wasn't moved).
            cam.reset();

            //This checks the input of the camera.  The parameter allows us to see
            //things like holding down a key, etc.
            HandleCameraInput((float)gameTime.ElapsedGameTime.TotalSeconds);

            if (cam.HasChanged)
            {
                //The camera has been moved, redraw screen.
                cameraHasChanged();
            }
        }

        public void cameraHasChanged()
        {
            b.BoardRotation = cam.Rotation;

            if (b.BoardZoom < 0.0f || b.BoardZoom > 8.0f)
            {
                b.BoardZoom = b.BoardZoom;
            }
            else
            {
                b.BoardZoom += cam.Zoom;
            }

            //At the end, call this because changes have been done.
            cam.reset();
        }

        public void resetPosition()
        {
            cam.Position = new Vector2(0.0f, 0.0f);
            //to get the board where you want it right at the start.
            cam.IsMovingUsingScreenAxis = false;
            cam.Rotation = 0f;
            cameraHasChanged(); //The camera has changed.
        }

        /// <summary>
        /// Initializes all of the board's tiles.
        /// Here is where you explain if a tile is a life tile
        /// or a pay day tile, etc.
        /// </summary>
        public void initializeTiles()
        {   
            //NOTE:  tile[0] is the very first tile in the array.  Because the game starts off
            //with two possible paths (college or career), tile[0] will represent the career path,
            //and therefore it will be the pay day tile. 
            //tile[14] will be the payday tile RIGHT after the stop sign where the two paths
            //meet.
            //With that in mind, the college path starts at tile[3].
            tiles[0] = new PayDayTile(new Vector2(-775.0f, 270.0f));
            tiles[1] = new LoseMoneySpace(5000, new Vector2(-775.0f, 360.0f)); 
            tiles[2] = new GainMoneySpace(10000, new Vector2(-833.0f, 458.0f)); 
            //COLLEGE PATH --Lose 40,000 dollars before you even begin!
            tiles[3] = new GainMoneySpace(20000, new Vector2(-778.0f, -90.0f));
            tiles[4] = new LoseMoneySpace(5000, new Vector2(-780.0f, -190.0f));
            tiles[5] = new LifeTile(new Vector2(-805.0f, -290.0f)); //GOOD
            tiles[6] = new GainMoneySpace(5000, new Vector2(-924.0f, -285.0f));
            tiles[7] = new LoseTurnTile(new Vector2(-950.0f, -190.0f));
            tiles[8] = new LifeTile(new Vector2(-950.0f, -90.0f)); 
            tiles[9] = new LoseMoneySpace(5000, new Vector2(-950.0f, 10.0f));
            tiles[10] = new LifeTile(new Vector2(-950.0f, 110.0f));
            tiles[11] = new LoseTurnTile(new Vector2(-950.0f, 210.0f));
            tiles[12] = new LifeTile(new Vector2(-950.0f, 310.0f));
            tiles[13] = new StopTile(1, new Vector2(-950.0f, 375.0f), ScreenManager);
            //NOTE:  THe career change and career choice tiles are the same thing
            //as they both use the same mechanism...
            tiles[14] = new PayDayTile(new Vector2(-950.0f, 479.0f));


            //This is after the payday after college
            tiles[15] = new LifeTile(new Vector2(-941.0f, 606.0f));
            tiles[16] = new LoseTurnTile(new Vector2(-884.0f, 660.0f));
            tiles[17] = new LifeTile(new Vector2(-784.0f, 656.0f));
            tiles[18] = new LoseMoneySpace(5000, new Vector2(-685.0f, 640.0f));
            tiles[19] = new GainMoneySpace(10000, new Vector2(-592.0f, 643.0f));
            tiles[20] = new LifeTile(new Vector2(-505.0f, 670.0f));
            tiles[21] = new LifeTile(new Vector2(-441.0f, 624.0f)); 
            tiles[22] = new PayDayTile(new Vector2(-494.0f, 560.0f));
            tiles[23] = new LoseTurnTile(new Vector2(-580.0f, 510.0f));
            tiles[24] = new StopTile(2, new Vector2(-645.0f, 430.0f), ScreenManager);
            tiles[25] = new LoseMoneySpace(10000, new Vector2(-593.0f, 330.0f));
            tiles[26] = new LifeTile(new Vector2(-485.0f, 351.0f));
            tiles[27] = new LoseMoneySpace(10000, new Vector2(-402.0f, 397.0f));
            tiles[28] = new LoseMoneySpace(10000, new Vector2(-320.0f, 444.0f));
            tiles[29] = new LoseMoneySpace(10000, new Vector2(-270.0f, 525.0f));
            tiles[30] = new LoseMoneySpace(10000, new Vector2(-242.0f, 623.0f));
            tiles[31] = new PayDayTile(new Vector2(-150, 670));
            tiles[32] = new TaxesDueSpace(new Vector2(-60, 626));
            tiles[33] = new GainMoneySpace(50000, new Vector2(-40.0f, 530));
            tiles[34] = new LifeTile(new Vector2(-40, 436));
            tiles[35] = new StopTile(3, new Vector2(-40, 346.0f), ScreenManager);
            tiles[36] = new PayDayTile(new Vector2(-40, 256));
            tiles[37] = new CareerChange(new Vector2(-40, 158), ScreenManager);
            tiles[38] = new BabySpace(1, new Vector2(-40, 66)); 
            tiles[39] = new LoseMoneySpace(5000, new Vector2(-40, -37)); 
            tiles[40] = new BabySpace(1, new Vector2(-40, -128)); 
            tiles[41] = new GainMoneySpace(10000, new Vector2(0, -225));
            tiles[42] = new PayDayTile(new Vector2(40, -338));
            tiles[43] = new BabySpace(2, new Vector2(39, -441));
            tiles[44] = new LoseMoneySpace(20000, new Vector2(63, -553));
            tiles[45] = new BabySpace(1, new Vector2(154, -636)); 
            tiles[46] = new LoseMoneySpace(5000, new Vector2(272, -640));
            tiles[47] = new LoseMoneySpace(40000, new Vector2(390, -650));

            //Turn Right
            tiles[48] = new LoseMoneySpace(5000, new Vector2(453, -563));
            tiles[49] = new TradeSalaryTile(new Vector2(576, -472), ScreenManager);
            tiles[50] = new BabySpace(1, new Vector2(502, -379));
            tiles[51] = new PayDayTile(new Vector2(609, -379));
            tiles[52] = new BabySpace(1, new Vector2(702, -379));
            tiles[53] = new LoseMoneySpace(15000, new Vector2(800, -379)); 
            tiles[54] = new LifeTile(new Vector2(916, -354));

            //Turn Left
            tiles[55] = new LoseMoneySpace(5000, new Vector2(511, -642));
            tiles[56] = new GainStock(new Vector2(650, -640), ScreenManager);
            tiles[57] = new LifeTile(new Vector2(750, -616));
            tiles[58] = new LifeTile(new Vector2(850, -580));
            tiles[59] = new PayDayTile(new Vector2(966, -537));
            tiles[60] = new LoseMoneySpace(15000, new Vector2(1000, -428));
            tiles[61] = new TradeSalaryTile(new Vector2(1000, -334), ScreenManager);

            tiles[62] = new LifeTile(new Vector2(970, -230));
            tiles[63] = new LifeTile(new Vector2(870, -180));
            tiles[64] = new LifeTile(new Vector2(770, -150));
            tiles[65] = new PayDayTile(new Vector2(670, -110));
            tiles[66] = new LoseMoneySpace(25000, new Vector2(645, 0));
            tiles[67] = new LoseMoneySpace(20000, new Vector2(645, 90));
            tiles[68] = new LifeTile(new Vector2(660, 200));
            tiles[69] = new LoseMoneySpace(20000, new Vector2(726, 283));
            tiles[70] = new PayDayTile(new Vector2(800, 350));
            tiles[71] = new LifeTile(new Vector2(876, 434));
            tiles[72] = new GainMoneySpace(10000, new Vector2(868, 522));
            tiles[73] = new SpinAgainTile(new Vector2(810, 580));
            tiles[74] = new TradeSalaryTile(new Vector2(740, 620), ScreenManager);
            tiles[75] = new TaxesDueSpace(new Vector2(615, 645));
            tiles[76] = new LoseMoneySpace(25000, new Vector2(504, 650));
            tiles[77] = new LoseMoneySpace(25000, new Vector2(412, 650));
            tiles[78] = new PayDayTile(new Vector2(330, 650));
            tiles[79] = new LoseStock(new Vector2(230, 650), ScreenManager);
            tiles[80] = new LifeTile(new Vector2(140, 646));
            tiles[81] = new LoseMoneySpace(5000, new Vector2(86, 590));
            tiles[82] = new GainMoneySpace(80000, new Vector2(86, 500));
            tiles[83] = new BabySpace(2, new Vector2(86, 400));

            //From top down direction: Turn left 
            tiles[84] = new LoseMoneySpace(15000, new Vector2(173, 350));
            tiles[85] = new PayDayTile(new Vector2(200, 240));
            tiles[86] = new GainMoneySpace(80000, new Vector2(200, 134));
            tiles[87] = new TaxesDueSpace(new Vector2(200, 40));

            //Go straight
            tiles[88] = new LoseMoneySpace(15000, new Vector2(85, 304));
            tiles[89] = new PayDayTile(new Vector2(85, 210));
            tiles[90] = new LifeTile(new Vector2(85, 120));
            tiles[91] = new LoseMoneySpace(35000, new Vector2(85, 30));
            tiles[92] = new TradeSalaryTile(new Vector2(85, -60), ScreenManager);
            tiles[93] = new LoseMoneySpace(25000, new Vector2(102, -160));
            tiles[94] = new GainMoneySpace(75000, new Vector2(180, -160));
            tiles[95] = new PayDayTile(new Vector2(195, -76));
            tiles[96] = new LoseMoneySpace(25000, new Vector2(280, -76)); 
            tiles[97] = new LifeTile(new Vector2(340, 10));
            tiles[98] = new GainMoneySpace(95000, new Vector2(340, 110));
            tiles[99] = new LoseMoneySpace(5000, new Vector2(340, 210));
            tiles[100] = new LifeTile(new Vector2(410, 256));
            tiles[101] = new LifeTile(new Vector2(484, 256));
            tiles[102] = new LoseMoneySpace(90000, new Vector2(564, 279));
            tiles[103] = new PayDayTile(new Vector2(560, 349));
            tiles[104] = new LoseMoneySpace(50000, new Vector2(485, 370));
            tiles[105] = new GainMoneySpace(100000, new Vector2(395, 380));
            tiles[106] = new TradeSalaryTile(new Vector2(356, 465), ScreenManager);
            tiles[107] = new LoseMoneySpace(30000, new Vector2(395, 550));
            tiles[108] = new LoseStock(new Vector2(500, 564), ScreenManager);
            tiles[109] = new LoseMoneySpace(125000, new Vector2(600, 530));
            tiles[110] = new PayDayTile(new Vector2(660, 460));
            tiles[111] = new LoseMoneySpace(25000, new Vector2(740, 400));
            tiles[112] = new TaxesDueSpace(new Vector2(825, 250));
            tiles[113] = new LoseMoneySpace(30000, new Vector2(918, 170));
            tiles[114] = new LoseMoneySpace(35000, new Vector2(964, 80));
            tiles[115] = new CareerChange(new Vector2(964, -20), ScreenManager); 
            tiles[116] = new SpinAgainTile(new Vector2(964, -122));
            tiles[117] = new PayDayTile(new Vector2(874, -264));
            tiles[118] = new LoseMoneySpace(100000, new Vector2(764, -270));
            tiles[119] = new TradeSalaryTile(new Vector2(670, -270), ScreenManager);
            tiles[120] = new LifeTile(new Vector2(580, -270));
            tiles[121] = new SpinAgainTile(new Vector2(484, -272));
            tiles[122] = new LoseMoneySpace(100000, new Vector2(380, -310));
            tiles[123] = new LoseMoneySpace(50000, new Vector2(325, -390));
            tiles[124] = new PayDayTile(new Vector2(290, -495));
            tiles[125] = new TaxesDueSpace(new Vector2(175, -495));
            tiles[126] = new SpinAgainTile(new Vector2(148, -395));
            tiles[127] = new LifeTile(new Vector2(105, -295));
            tiles[128] = new LoseMoneySpace(125000, new Vector2(-70, -275));
            tiles[129] = new LifeTile(new Vector2(-145, -315));
            tiles[130] = new TradeSalaryTile(new Vector2(-187, -395), ScreenManager);
            tiles[131] = new PayDayTile(new Vector2(-190, -500));
            tiles[132] = new LifeTile(new Vector2(-170, -630));
            tiles[133] = new SpinAgainTile(new Vector2(-273, -665));
            tiles[134] = new LoseMoneySpace(65000, new Vector2(-370, -665));
            tiles[135] = new LifeTile(new Vector2(-460, -665));
            tiles[136] = new PayDayTile(new Vector2(-555, -665));
            tiles[137] = new LifeTile(new Vector2(-640, -665));
            tiles[138] = new SpinAgainTile(new Vector2(-730, -665));
            tiles[139] = new LifeTile(new Vector2(-830, -665));
            tiles[140] = new LoseMoneySpace(45000, new Vector2(-940, -637));
            tiles[141] = new LifeTile(new Vector2(-960, -540));
            tiles[142] = new PayDayTile(new Vector2(-940, -440));
            tiles[143] = new LoseMoneySpace(35000, new Vector2(-830, -440));
            tiles[144] = new LoseMoneySpace(55000, new Vector2(-780, -550));
            tiles[145] = new GainMoneySpace(20000, new Vector2(-675, -552));  
            tiles[146] = new RetireSpace(new Vector2(-675.0f, -400.0f));

            for (int i = 0; i < MAX_TILES; i++)
            {
                tiles[i].Number = i;
            }
        }

        public void initializeCareers()
        {
            careers[0] = new CareerCard("Teacher", true, 20000, "Green", ScreenManager);
            careers[1] = new CareerCard("Salesperson", false, 12000, "Red", ScreenManager);
            careers[2] = new CareerCard("Doctor", true, 35000, "Blue", ScreenManager);
            careers[3] = new CareerCard("Artist", false, 15000, "Brown", ScreenManager);
            careers[4] = new CareerCard("Athlete", false, 25000, "Yellow", ScreenManager);
            careers[5] = new CareerCard("Computer Consultant", true, 25000, "Red", ScreenManager);
            careers[6] = new CareerCard("Police Officer", true, 20000, "Orange", ScreenManager);
        }

        int runs = 0;
        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            Player p;

            if (input == null)
                throw new ArgumentNullException("Input was null.");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;
            PlayerIndex plIndex;

            KeyboardState keyboardState = input.currKeyboardStates[playerIndex];
            GamePadState gamePadState = input.currGamepadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamepadConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                string msg = "Select which path to take, College or Career.";
                SelectPathScreen selectPath = new SelectPathScreen(msg, true);
                selectPath.CareerPath += CareerPathSelected;
                selectPath.CollegePath += CollegePathSelected;

                #region Calculation of first move - works
                if (isFirstMoveOfGame() && !firstHasBeenDetermined)
                {
                    //Hit to S find out!
                    if (input.isNewKeyPress(Keys.S, ControllingPlayer, out plIndex))
                    {
                        plArray[currPlayer].SpinWheel();
                        if(plArray[currPlayer].CurrentSpin >= largestSpin)
                            largestSpin = plArray[currPlayer].CurrentSpin;
                        currPlayer++;
                    }
                    if (currPlayer >= n_players)
                    {
                        for (int n = 0; n < n_players; n++)
                        {
                            if (plArray[n].CurrentSpin == largestSpin)
                                currPlayer = n;
                        }
                        firstHasBeenDetermined = true;
                    }
                }
                #endregion

                //Initiate turn calculations.
                if (firstHasBeenDetermined)
                {
                    if (currPlayer == n_players)
                        currPlayer--;
                    tempPlayerIndex = currPlayer;
                    p = plArray[currPlayer];
                    if (!p.IsRetired)
                    {
                        if (input.isNewKeyPress(Keys.B, ControllingPlayer, out plIndex))
                        {
                            ScreenManager.AddScreen(bs, null);
                        }
                        if (!p.HasChosenPath)
                            ScreenManager.AddScreen(selectPath, null);
                        if (input.isNewKeyPress(Keys.S, ControllingPlayer, out plIndex))
                        {
                            p.SpinWheel();
                            hasSpun = true;

                            #region Stock Checking
                            for (int a = 0; a < n_players; a++)
                            {
                                for (int v = 0; v < plArray[a].StockCount; v++)
                                {
                                    if (p.CurrentSpin == plArray[a].StockNumberArray[v])
                                    {
                                        p.Balance += 10000;
                                        bank.AmountDispensed -= 10000;
                                    }
                                }
                            }
                            #endregion

                            #region End of Path checking
                            //Check if player is at the end, so we don't exceed the end of the
                            //tiles array.
                            if (p.CurrentTile + p.CurrentSpin > 146)
                            {
                                if (checkForStopTile(p.CurrentTile, 146, out stopSignLocation))
                                {
                                    p.moveTo(tiles[stopSignLocation]);
                                    tiles[p.CurrentTile].performTileAction(ref plArray, currPlayer, n_players, ref bank);
                                }
                                //No stop tile found.
                                else
                                {
                                    //Check for payday tile.
                                    for (int u = p.CurrentTile; u <= 146; u++)
                                    {
                                        if (tiles[u] is PayDayTile)
                                            tiles[u].performTileAction(ref plArray, currPlayer, n_players, ref bank);
                                    }
                                    p.moveTo(tiles[146]); //<---moveTo changes currentTile to argument.
                                    tiles[p.CurrentTile].performTileAction(ref plArray, currPlayer, n_players, ref bank);
                                }
                            }
                            #endregion

                            #region Not at end of path
                            //Here we must check for multiple patways, because there are no
                            //such choices near the end of the path.
                            else
                            {
                                //Choose correct path for the career players.
                                #region Career Choice Path Correction
                                if (playerIsBetweenTiles(0, 2, currPlayer) && p.CurrentTile + p.CurrentSpin > 2)
                                {
                                    //This means the user should move through past tile 14!
                                    if (p.CurrentTile == 0)
                                    {
                                        //tile 0 is a payday tile.
                                        tiles[0].performTileAction(ref plArray, currPlayer, n_players, ref bank);
                                        p.moveTo(tiles[14 + (p.CurrentSpin - 3)]);
                                    }
                                    else if (p.CurrentTile == 1)
                                    {
                                        p.moveTo(tiles[14 + (p.CurrentSpin - 2)]);
                                        for (int i = 14; i <= p.CurrentSpin - 2; i++)
                                        {
                                            //Check for a pay day tile.
                                            if (tiles[i] is PayDayTile)
                                                tiles[i].performTileAction(ref plArray, currPlayer, n_players, ref bank);
                                        }
                                    }
                                    else if (p.CurrentTile == 2)
                                    {
                                        p.moveTo(tiles[14 + (p.CurrentSpin - 1)]);
                                        for (int i = 14; i <= p.CurrentSpin - 1; i++)
                                        {
                                            //Check for a pay day tile.
                                            if (tiles[i] is PayDayTile)
                                                tiles[i].performTileAction(ref plArray, currPlayer, n_players, ref bank);
                                        }
                                    }
                                }
                                #endregion
                                #region First Path Decision
                                else if (playerIsBetweenTiles(37, 47, currPlayer) && p.CurrentSpin + p.CurrentTile > 47)
                                {
                                    //Add a new select left/right screen.
                                    tempPlayerIndex = currPlayer;
                                    tileSplitIndex = 47; //tempTileIndex holds the tile at which the split occurs.
                                    ScreenManager.AddScreen(slr, null);
                                }
                                #endregion
                                #region Second Path Decision
                                else if (playerIsBetweenTiles(73, 83, currPlayer) && p.CurrentSpin + p.CurrentTile > 83)
                                {
                                    //Add a new select left/right screen for the second decision.
                                    tempPlayerIndex = currPlayer;
                                    tileSplitIndex = 83;
                                    ScreenManager.AddScreen(slr, null);
                                }
                                #endregion
                                //Now check to see if you are on a straight path WITHIN A PATH CHOSEN
                                //When the box comes up selected which path to choose, 
                                //there is a chance you will not actually pass the whole path.
                                //This is where that is corrected on the next turn.
                                #region Player in first path and exits out
                                else if (playerIsBetweenTiles(48, 54, currPlayer) && (p.CurrentSpin + p.CurrentTile) > 54)
                                {
                                    //Check for pay day within path
                                    for (int i = p.CurrentTile; i <= 54; i++)
                                    {
                                        if (tiles[i] is PayDayTile)
                                            tiles[i].performTileAction(ref plArray, currPlayer, n_players, ref bank);
                                    }
                                    //Check for pay day after path.
                                    for (int i = 61; i <= (61 + (p.CurrentSpin - ((54 - p.CurrentTile) + 1))); i++)
                                    {
                                        if (tiles[i] is PayDayTile)
                                            tiles[i].performTileAction(ref plArray, currPlayer, n_players, ref bank);
                                    }
                                    p.moveTo(tiles[(61 + (p.CurrentSpin - ((54 - p.CurrentTile) + 1)))]);
                                }
                                #endregion
                                #region Player in second path and exits out
                                else if (playerIsBetweenTiles(84, 87, currPlayer) && (p.CurrentSpin + p.CurrentTile) > 87)
                                {
                                    for (int i = p.CurrentTile; i <= 87; i++)
                                    {
                                        if (tiles[i] is PayDayTile)
                                            tiles[i].performTileAction(ref plArray, currPlayer, n_players, ref bank);
                                    }
                                    //Check for pay day after path.
                                    for (int i = 95; i <= (95 + (p.CurrentSpin - ((87 - p.CurrentTile) + 1))); i++)
                                    {
                                        if (tiles[i] is PayDayTile)
                                            tiles[i].performTileAction(ref plArray, currPlayer, n_players, ref bank);
                                    }
                                    p.moveTo(tiles[(95 + (p.CurrentSpin - ((87 - p.CurrentTile) + 1)))]);
                                }
                                #endregion
                                //Assuming there is no break, a straight path with no choices..
                                else if (checkForStopTile(p.CurrentTile + 1, p.CurrentTile + p.CurrentSpin, out stopSignLocation))
                                {
                                    p.moveTo(tiles[stopSignLocation]);
                                    tiles[p.CurrentTile].performTileAction(ref plArray, currPlayer, n_players, ref bank);
                                }
                                else
                                {
                                    //Check for a pay day tile up until ONE MINUS the tile you land on
                                    //in case the tile you land on is itself a pay day tile.
                                    for (int u = p.CurrentTile + 1; u < p.CurrentTile + p.CurrentSpin; u++)
                                    {
                                        if (tiles[u] is PayDayTile)
                                            tiles[u].performTileAction(ref plArray, currPlayer, n_players, ref bank);
                                    }
                                    p.moveTo(tiles[p.CurrentSpin + p.CurrentTile]);
                                    //Perform action of tile you land on.
                                    tiles[p.CurrentTile].performTileAction(ref plArray, currPlayer, n_players, ref bank);
                                }
                            }
                            #endregion
                        }
                    }
                    else if (p.IsRetired)
                        hasSpun = true;

                    #region Alternate Turn
                    if (hasSpun == true)
                    {
                        if (currPlayer == n_players - 1)
                            currPlayer = 0;
                        else
                            currPlayer++;
                        hasSpun = false;
                    }
                    #endregion
                }
                #region Calculate victory
                if (allPlayersRetired())
                {
                    //Calculate total wealth.
                    int largestGrandTotal = 0;
                    int winningIndex = 0;
                    /*
                    for (int i = 0; i < n_players; i++)
                    {
                        plArray[i].GrandTotal += plArray[i].Balance;
                        plArray[i].GrandTotal -= plArray[i].AmountOwed;
                        for (int j = 0; j < plArray[i].LifeCardCount; j++)
                        {
                            plArray[i].GrandTotal += plArray[i].LifeCards[j].Amount;
                        }
                    }*/

                    for (int i = 0; i < n_players; i++)
                    {
                        if (plArray[i].GrandTotal > largestGrandTotal)
                        {
                            winningIndex = i;
                            largestGrandTotal = plArray[i].GrandTotal;
                        }
                    }
                    vs = new VictoryScreen("Congratulations player " + (winningIndex + 1) + ", you won!");
                    vs.ConfirmExit += VictoryExit;
                    ScreenManager.AddScreen(vs, null);
                }
                #endregion
            }
        }

        #region Bought Insurance
        void InsuranceBought(object sender, PlayerIndexEventArgs e)
        {
            plArray[tempPlayerIndex].IsInsured = true;
        }
        #endregion

        #region Bought Stock
        void StockBought(object sender, PlayerIndexEventArgs e)
        {
            StockScreen ss = new StockScreen("Buy a stock!");
            ss.StockOne += FirstStock;
            ss.StockTwo += SecondStock;
            ss.StockThree += ThirdStock;
            ss.StockFour += FourthStock;
            ss.StockFive += FifthStock;
            ss.StockSix += SixthStock;
            ss.StockSeven += SeventhStock;
            ss.StockEight += EighthStock;
            ss.StockNine += NinthStock;
            ss.StockTen += TenthStock;
            ScreenManager.AddScreen(ss, null);
        }

        #region Event handlers
        void FirstStock(object sender, PlayerIndexEventArgs e)
        {
            plArray[tempPlayerIndex].addStock(new Stock(1));
        }

        void SecondStock(object sender, PlayerIndexEventArgs e)
        {
            plArray[tempPlayerIndex].addStock(new Stock(2));
        }

        void ThirdStock(object sender, PlayerIndexEventArgs e)
        {
            plArray[tempPlayerIndex].addStock(new Stock(3));
        }

        void FourthStock(object sender, PlayerIndexEventArgs e)
        {
            plArray[tempPlayerIndex].addStock(new Stock(4));
        }

        void FifthStock(object sender, PlayerIndexEventArgs e)
        {
            plArray[tempPlayerIndex].addStock(new Stock(5));
        }

        void SixthStock(object sender, PlayerIndexEventArgs e)
        {
            plArray[tempPlayerIndex].addStock(new Stock(6));
        }

        void SeventhStock(object sender, PlayerIndexEventArgs e)
        {
            plArray[tempPlayerIndex].addStock(new Stock(7));
        }

        void EighthStock(object sender, PlayerIndexEventArgs e)
        {
            plArray[tempPlayerIndex].addStock(new Stock(8));
        }

        void NinthStock(object sender, PlayerIndexEventArgs e)
        {
            plArray[tempPlayerIndex].addStock(new Stock(9));
        }

        void TenthStock(object sender, PlayerIndexEventArgs e)
        {
            plArray[tempPlayerIndex].addStock(new Stock(10));
        }
        #endregion
        #endregion

        #region Chose Left
        void ChoseLeftPath(object sender, PlayerIndexEventArgs e)
        {
            //If the split is at 47...
            if (tileSplitIndex == 47) //Means he chose to go straight
            {
                //Check for pay day before tile 47.
                for (int i = plArray[tempPlayerIndex].CurrentTile; i <= 47; i++)
                {
                    if (tiles[i] is PayDayTile)
                        tiles[i].performTileAction(ref plArray, tempPlayerIndex, n_players, ref bank);
                }
                //Check for pay day tile ater tile 47.
                for (int i = 55; i <= 55 + (plArray[tempPlayerIndex].CurrentSpin - (47 - plArray[tempPlayerIndex].CurrentTile)); i++)
                {
                    if (tiles[i] is PayDayTile)
                        tiles[i].performTileAction(ref plArray, tempPlayerIndex, n_players, ref bank);
                }
                //My gosh this is complex.
                plArray[tempPlayerIndex].moveTo(tiles[55 + (plArray[tempPlayerIndex].CurrentSpin - (47 - plArray[tempPlayerIndex].CurrentTile))]);
            }
            else if (tileSplitIndex == 83)
            {
                //Check for pay day before tile 83.
                for (int i = plArray[tempPlayerIndex].CurrentTile; i <= 83; i++)
                {
                    if (tiles[i] is PayDayTile)
                        tiles[i].performTileAction(ref plArray, tempPlayerIndex, n_players, ref bank);
                }
                //Check for pay day after tile 83.
                for (int i = 88; i <= 88 + (plArray[tempPlayerIndex].CurrentSpin - (83 - plArray[tempPlayerIndex].CurrentTile)); i++)
                {
                    if (tiles[i] is PayDayTile)
                        tiles[i].performTileAction(ref plArray, tempPlayerIndex, n_players, ref bank);
                }
                plArray[tempPlayerIndex].moveTo(tiles[88+(plArray[tempPlayerIndex].CurrentSpin - (83 - plArray[tempPlayerIndex].CurrentTile))]);
            }
        }
        #endregion

        #region Chose Right
        void ChoseRightPath(object sender, PlayerIndexEventArgs e)
        {
            //If split is at 47
            if (tileSplitIndex == 47)
            {
                //Going the right path changes very little.
                //Except that you go to 48, 49, 50,51,52,53,54,61!
                //Check payday before tile 47
                for (int i = plArray[tempPlayerIndex].CurrentTile; i <= 47; i++)
                {
                    if (tiles[i] is PayDayTile)
                        tiles[i].performTileAction(ref plArray, tempPlayerIndex, n_players, ref bank);
                }
                //Player lies somewhere before tile 61.
                if (48 + (plArray[tempPlayerIndex].CurrentSpin - (48 - plArray[tempPlayerIndex].CurrentTile)) <= 54)
                {
                    //Check for pay day
                    for (int i = 48; i <= (48 + (plArray[tempPlayerIndex].CurrentSpin - (48 - plArray[tempPlayerIndex].CurrentTile))); i++)
                    {
                        if (tiles[i] is PayDayTile)
                            tiles[i].performTileAction(ref plArray, tempPlayerIndex, n_players, ref bank);
                    }
                    plArray[tempPlayerIndex].moveTo(tiles[(48 + (plArray[tempPlayerIndex].CurrentSpin - (48 - plArray[tempPlayerIndex].CurrentTile)))]);
                }
                else //Spun past entire path!
                {
                    for (int i = 48; i <= 54; i++)
                    {
                        if (tiles[i] is PayDayTile)
                            tiles[i].performTileAction(ref plArray, tempPlayerIndex, n_players, ref bank);
                    }
                    for (int i = 61; i <= (61 + (plArray[tempPlayerIndex].CurrentSpin - 7 - (48 - plArray[tempPlayerIndex].CurrentTile))); i++)
                    {
                        if (tiles[i] is PayDayTile)
                            tiles[i].performTileAction(ref plArray, tempPlayerIndex, n_players, ref bank);
                    }
                    plArray[tempPlayerIndex].moveTo(tiles[(61 + (plArray[tempPlayerIndex].CurrentSpin - 7 - (48 - plArray[tempPlayerIndex].CurrentTile)))]);
                }
            }
            else if (tileSplitIndex == 83)
            {
                //Before tile 83.
                for (int i = plArray[tempPlayerIndex].CurrentTile; i <= 83; i++)
                {
                    if (tiles[i] is PayDayTile)
                        tiles[i].performTileAction(ref plArray, tempPlayerIndex, n_players, ref bank);
                }
                //Is the player in the path?
                if (84 + (plArray[tempPlayerIndex].CurrentSpin - (84 - plArray[tempPlayerIndex].CurrentTile)) <= 87)
                {
                    //Check pay day
                    for (int i = 84; i <= (84 + (plArray[tempPlayerIndex].CurrentSpin - (84 - plArray[tempPlayerIndex].CurrentTile))); i++)
                    {
                        if (tiles[i] is PayDayTile)
                            tiles[i].performTileAction(ref plArray, tempPlayerIndex, n_players, ref bank);
                    }
                    plArray[tempPlayerIndex].moveTo(tiles[(84 + (plArray[tempPlayerIndex].CurrentSpin - (84 - plArray[tempPlayerIndex].CurrentTile)))]);
                }
                else //Spun past whole path!
                {
                    for (int i = 84; i <= 87; i++)
                    {
                        if (tiles[i] is PayDayTile)
                            tiles[i].performTileAction(ref plArray, tempPlayerIndex, n_players, ref bank);
                    }
                    for (int i = 95; i <= (95 + (plArray[tempPlayerIndex].CurrentSpin - 4 - (84 - plArray[tempPlayerIndex].CurrentTile))); i++)
                    {
                        if (tiles[i] is PayDayTile)
                            tiles[i].performTileAction(ref plArray, tempPlayerIndex, n_players, ref bank);
                    }
                    plArray[tempPlayerIndex].moveTo(tiles[(95 + (plArray[tempPlayerIndex].CurrentSpin - 4 - (84 - plArray[tempPlayerIndex].CurrentTile)))]);
                }
            }
        }
        #endregion

        #region Between tiles
        /// <summary>
        /// Checks to see if the player is between two tiles, inclusive.
        /// </summary>
        /// <param name="lowerTile">On or after this tile, inclusive.</param>
        /// <param name="upperTile">On or before this tile, inclusive.</param>
        /// <param name="n">Which player?</param>
        /// <returns></returns>
        bool playerIsBetweenTiles(int lowerTile, int upperTile, int n)
        {
            if (plArray[n].CurrentTile >= lowerTile && plArray[n].CurrentTile <= upperTile)
                return true;
            else
                return false;
        }
        #endregion

        #region Check For Stop Tile
        bool checkForStopTile(int lower, int upper, out int loc)
        {
            for (int i = lower; i <= upper; i++)
            {
                if (tiles[i] is StopTile)
                {
                    loc = i;
                    return true;
                }
            }
            loc = -1;
            return false;
        }
        #endregion

        #region All Players Retired
        bool allPlayersRetired()
        {
            int retiredCount = 0;

            for (int i = 0; i < n_players; i++)
            {
                if (plArray[i].IsRetired == true)
                    retiredCount++;
            }
            if (retiredCount == n_players) //All players are retired!
                return true;
            else
                return false;
        }
        #endregion

        #region Is First Move
        bool isFirstMoveOfGame()
        {
            for (int i = 0; i < n_players; i++)
            {
                if (plArray[i].CurrentTile > 0)
                    return false;
            }
            return true;
        }
        #endregion

        #region Event Results
        void VictoryExit(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenu());
        }

        void CollegePathSelected(object sender, PlayerIndexEventArgs e)
        {
            plArray[tempPlayerIndex].Balance -= 40000;
            plArray[tempPlayerIndex].moveTo(tiles[3]);
            plArray[tempPlayerIndex].Degreed = true;
            plArray[tempPlayerIndex].HasChosenPath = true;
        }

        void CareerPathSelected(object sender, PlayerIndexEventArgs e)
        {
            plArray[tempPlayerIndex].moveTo(tiles[0]);
            plArray[tempPlayerIndex].Degreed = false;
            Random r = new Random();
            plArray[tempPlayerIndex].Career = new CareerCard(careers[r.Next(0, 6)]);
            while (plArray[tempPlayerIndex].Career.DegreeRequired)
            {
                //Make sure it doesn't need a degree.
                plArray[tempPlayerIndex].Career = new CareerCard(careers[r.Next(0, 6)]);
            }
            plArray[tempPlayerIndex].Career.selectSalary(50000);
            plArray[tempPlayerIndex].HasChosenPath = true;
        }
        #endregion

        #region Handle Camera Input
        /// <summary>
        /// Handles input for the camera, such as rotation, zoom, etc.
        /// </summary>
        /// <param name="timePassed">The total amount of time elapsed, in seconds.</param>
        public void HandleCameraInput(float timePassed)
        {
            float x_pan = 0.0f, y_pan = 0.0f; //For the camera.

            if (Keyboard.GetState().IsKeyDown(Keys.Up)||
                Mouse.GetState().Y < 100)
            {
                y_pan += 1.0f * timePassed * movementRate;
                cam.moveUp(ref y_pan);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                y_pan -= 1.0f * timePassed * movementRate;
                cam.moveUp(ref y_pan);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                x_pan -= 1.0f * timePassed * movementRate;
                cam.moveRight(ref x_pan);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                x_pan += 1.0f * timePassed * movementRate;
                cam.moveRight(ref x_pan);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                if (b.BoardZoom < 2.0f)
                {
                    b.BoardZoom += 0.01f;
                    Vector2 zoomPos;
                    for (int i = 0; i < n_players; i++)
                    {

                        zoomPos = plArray[i].Position;
                        zoomPos.X *=  b.BoardZoom;
                        zoomPos.Y *= b.BoardZoom;
                        plArray[i].Position = zoomPos;
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                if (b.BoardZoom > 0.5f)
                { //Don't zoom out too far.
                    b.BoardZoom -= 0.01f;
                    Vector2 zoomPos;
                    for (int i = 0; i < n_players; i++)
                    {

                        zoomPos = plArray[i].Position;
                        zoomPos.X /= b.BoardZoom;
                        zoomPos.Y /= b.BoardZoom;
                        plArray[i].Position = zoomPos;
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemComma) || 
                Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                //Ok, so let's explain this.
                //Whenever we change this value in the camera, 
                //the value HasChanged becomes true.  In the update
                //method there is a check for this value, which then
                //redraws the board.
                cam.Rotation += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemPeriod))
            {
                cam.Rotation -= 0.1f;
            }
        }

        #endregion

        #region Draw
        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Aqua, 0, 0);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            imagePosition = cam.Position;
            b.Draw(imagePosition); //Draw the board.
            for (int i = 0; i < n_players; i++)
            {
                plArray[i].Draw(cam.Position - plArray[i].Position);
            }
            hud.Draw(plArray, n_players);
            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeToBlack(255 - TransitionAlpha);
        }
        #endregion

        #endregion
    }
}

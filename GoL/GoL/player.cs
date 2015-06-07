
#region Class Description
/* This is the player class.  It represents a player on the board.
 * This player moves to a specific tile, retires, etc.
 * */
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace GoL
{
    class Player
    {
        #region Declarations
        Texture2D chosenPiece; 
        int amountOwed;
        int balance;
        int childAmount;
        int salary;
        int previousSpin;  //What did this player spin before?
        int currentSpin;
        bool isWinner;
        bool isRetired;
        SpriteBatch sBatch; //For drawing the dot.
        Vector2 position; //Where the player is, in XY coords.
        //25 total lifecards the player can have.
        LifeCard [] lifeCards;
        int lifeCardCount = 0;
        bool hasLostTurn = false;
        CareerCard career; //Players have careers.
        Color chosenColor;
        int currentTile; //What tile the player is at.
        bool degreed;
        bool hasChosenPath;
        bool isMarried;
        bool hasHouse;
        bool isInsured;
        int grandTotal;
        int [] stockNumbers;
        int stockCount;
        #endregion

        #region Properties
        public Texture2D ChosenPiece
        {
            get
            {
                return chosenPiece;
            }
        }

        public int AmountOwed
        {
            get
            {
                return amountOwed;
            }
            set
            {
                amountOwed = value;
            }
        }

        public int Balance
        {
            get
            {
                return balance;
            }
            set
            {
                balance = value;
            }
        }

        public int ChildAmount
        {
            get
            {
                return childAmount;
            }
            set
            {
                childAmount = value;
            }
        }

        public int PreviousSpin
        {
            get
            {
                return previousSpin;
            }
            set
            {
                previousSpin = value;
            }
        }

        /// <summary>
        /// Gets the result of the spin function.
        /// In order to get a different value, you must call spin() first.
        /// </summary>
        public int CurrentSpin
        {
            get
            {
                return currentSpin;
            }
            set
            {
                currentSpin = value;
            }
        }

        public bool IsWinner
        {
            get
            {
                return isWinner;
            }
            set
            {
                isWinner = value;
            }
        }

        public bool IsRetired
        {
            get
            {
                return isRetired;
            }
            set
            {
                isRetired = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public bool LostTurn
        {
            get
            {
                return hasLostTurn;
            }
            set
            {
                hasLostTurn = value;
            }
        }

        public CareerCard Career
        {
            get
            {
                return career;
            }
            set
            {
                career = value;
            }
        }

        public Color ChosenColor
        {
            get
            {
                return chosenColor;
            }
            set
            {
                chosenColor = value;
            }
        }

        public int CurrentTile
        {
            get
            {
                return currentTile;
            }
            set
            {
                currentTile = value;
            }
        }

        public SpriteBatch SBatch
        {
            get
            {
                return sBatch;
            }
        }

        public LifeCard[] LifeCards
        {
            get
            {
                return lifeCards;
            }
        }

        public bool Degreed
        {
            get
            {
                return degreed;
            }
            set
            {
                degreed = value;
            }
        }

        public bool HasChosenPath
        {
            get
            {
                return hasChosenPath;
            }
            set
            {
                hasChosenPath = value;
            }
        }

        public bool IsMarried
        {
            get
            {
                return isMarried;
            }
            set
            {
                isMarried = value;
            }
        }

        public Salary Salary
        {
            get
            {
                return Career.Salary;
            }
            set
            {
                Career.Salary = value;
            }
        }

        public bool HasHouse
        {
            get
            {
                return hasHouse;
            }
            set
            {
                hasHouse = value;
            }
        }

        public bool IsInsured
        {
            get
            {
                return isInsured;
            }
            set
            {
                isInsured = value;
            }
        }

        public int LifeCardCount
        {
            get
            {
                return lifeCardCount;
            }
            set
            {
                lifeCardCount = value;
            }
        }

        public int GrandTotal
        {
            get
            {
                return grandTotal;
            }
            set
            {
                grandTotal = value;
            }
        }

        public int[] StockNumberArray
        {
            get
            {
                return stockNumbers;
            }
        }

        public int StockCount
        {
            get
            {
                return stockCount;
            }
            set
            {
                stockCount = value;
            }
        }
        #endregion

        #region Initializer
        public Player(Texture2D piece, GraphicsDevice g)
        {
            chosenPiece = piece;
            chosenColor = new Color(Color.White, 1.0f);
            sBatch = new SpriteBatch(g);
            IsRetired = false;
            IsWinner = false;
            StockCount = 0;
            ChildAmount = 0;
            //Salary = 0;
            Balance = 0;
            AmountOwed = 0;
            lifeCards = new LifeCard[25];
            position = new Vector2(500.0f, 500.0f);
            CurrentTile = 0;
            isMarried = false;
            degreed = false;
            hasChosenPath = false;
            Career = new CareerCard(); //Default career card.
            hasHouse = false;
            isInsured = false;
            grandTotal = 0;
            stockNumbers = new int[90];
            for (int i = 0; i < 10; i++)
            {
                stockNumbers[i] = -1;
            }
            stockCount = 0;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="pl">The player who'se attributes to copy.</param>
        public Player(Player pl)
        {
            chosenPiece = pl.ChosenPiece;
            ChosenColor = pl.ChosenColor;
            sBatch = pl.SBatch;
            IsRetired = pl.IsRetired;
            IsWinner = pl.IsWinner;
            ChildAmount = pl.ChildAmount;
            Balance = pl.Balance;
            AmountOwed = pl.AmountOwed;
            position = pl.Position;
            CurrentTile = pl.CurrentTile;
            CurrentSpin = pl.CurrentSpin;
            PreviousSpin = pl.PreviousSpin;
            Degreed = pl.Degreed;
            HasChosenPath = pl.HasChosenPath;
            isMarried = pl.IsMarried;
            Career = pl.Career;
            Career.Salary = pl.Career.Salary;
            hasHouse = pl.hasHouse;
            IsInsured = pl.IsInsured;
            lifeCardCount = pl.LifeCardCount;
            lifeCards = new LifeCard[25];
            for (int i = 0; i < lifeCardCount; i++)
            {
                LifeCards[i] = new LifeCard(pl.LifeCards[i]);
            }
            grandTotal = pl.GrandTotal;
            stockCount = pl.StockCount;
            stockNumbers = new int[90];
            for (int i = 0; i < stockCount; i++)
            {
                stockNumbers[i] = -1;
            }
            for (int i = 0; i < stockCount; i++)
            {
                stockNumbers[i] = pl.StockNumberArray[i];
            }
        }
        #endregion

        #region Update
        public void Update(Vector2 p)
        {
            position = p;
        }
        #endregion

        #region Draw
        public void Draw()
        {
            sBatch.Begin();
            //Draw the sprite at the specific location.
            sBatch.Draw(chosenPiece, position, chosenColor);
            sBatch.End();
        }

        public void Draw(Vector2 pos)
        {
            sBatch.Begin();
            sBatch.Draw(chosenPiece, pos, chosenColor);
            sBatch.End();
        }
        #endregion

        #region Public Methods
        //MoveTo method...
        public void SpinWheel()
        {
            if (!isRetired && !hasLostTurn)
            {
                Random r = new Random();
                PreviousSpin = CurrentSpin;
                CurrentSpin = r.Next(1, 11); 
                //Spin a random number between 1 and 10.
                //Basically, this returns how many tiles to move up by in the
                //tile array.  So if the player is at Tile 45, then in the array the player
                //is at tile[44].  If player spins 5, then they need to go to tile 50, or
                //tile[49].
            }
        }

        public void moveTo(Tile t)
        {
            //Check tiles to find payday or stop sign.
            position = t.Position;
            CurrentTile = t.Number;
            t.IsOccupied = true;
        }

        public void addLifeCard()
        {
            lifeCards[lifeCardCount] = new LifeCard();
            lifeCardCount++;
        }

        public void addStock(Stock s)
        {
            stockNumbers[stockCount] = s.Number;
            stockCount++;
        }
        #endregion
    }
}

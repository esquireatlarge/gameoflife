
#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace GoL
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Declarations
        GraphicsDeviceManager graphics;
        //SpriteBatch spriteBatch;
        //Texture2D grayTrack;
        //Texture2D playerBlue;
        //Texture2D board;
        //Vector2 playerPosition = new Vector2(120, 197);
        //RenderTarget2D trackRender;
        ScreenManager sMan;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            sMan = new ScreenManager(this);
            Components.Add(sMan);

            sMan.AddScreen(new BackgroundScreen(), null);
            sMan.AddScreen(new MainMenu(), null);

            graphics.PreferredBackBufferHeight = 1020;
            graphics.PreferredBackBufferWidth = 768;
            graphics.IsFullScreen = false; //Only the hud is affected by full screen!!
            this.IsMouseVisible = true;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        /*protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferHeight = 1024;
            graphics.PreferredBackBufferWidth = 768;
            graphics.IsFullScreen = true;
            this.IsMouseVisible = true;
            graphics.ApplyChanges();

            base.Initialize();
        }*/

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
          /*  spriteBatch = new SpriteBatch(GraphicsDevice);
            grayTrack = Content.Load<Texture2D>("boardgray");
            board = Content.Load<Texture2D>("board");
            playerBlue = Content.Load<Texture2D>("testplayerdot");
            trackRender = new RenderTarget2D(graphics.GraphicsDevice, playerBlue.Width + 100,
                playerBlue.Height + 100, 1, SurfaceFormat.Color);
            // TODO: use this.Content to load your game content here
           * */
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /*protected override void Update(GameTime gameTime)
        {
            int movey = 0;
            int movex = 0;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();
            // TODO: Add your update logic here
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.F))
            {
                if(graphics.IsFullScreen)
                    graphics.ToggleFullScreen();
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up))
            {
                //playerPosition.Y -= 2;
                movey -= 2;
            }
            else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
            {
                //playerPosition.Y += 2;
                movey += 2;
            }
            else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
            {
                //playerPosition.X += 2;
                movex += 2;
            }
            else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
            {
                //playerPosition.X -= 2;
                movex -= 2;
            }

            Rectangle spin = new Rectangle(30, 200, 50, 50);

            Mouse.SetPosition(spin.Center.X, spin.Center.Y);

            if (Mouse.GetState().X > 30 && Mouse.GetState().X < 80 &&
                Mouse.GetState().Y > 200 && Mouse.GetState().Y < 250)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    playerPosition.X += 50;
                    playerPosition.Y = 90;
                    playerPosition.X += movex;
                    playerPosition.Y += movey;
                }
            }
            //if (!collisionDetected(movex) || !collisionDetected(movey))
            //{
                //playerPosition.X += movex;
                //playerPosition.Y += movey;
            //}
            base.Update(gameTime);
        }*/

        /*
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(grayTrack, new Rectangle(0, 0, 1000, 1000), Color.White);
            spriteBatch.Draw(board, new Rectangle(0, 0, 1000, 800), Color.White);
            spriteBatch.Draw(playerBlue, new Rectangle((int)playerPosition.X, (int)playerPosition.Y, playerBlue.Width, playerBlue.Height), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }*/

        /*private bool collisionDetected(int aMove)
        {

            //Calculate the Position of the Car and create the collision Texture. This texture will contain

            //all of the pixels that are directly underneath the sprite currently on the Track image.

            float aXPosition = (float)(-playerBlue.Width / 2 + playerPosition.X + aMove);

            float aYPosition = (float)(-playerBlue.Height / 2 + playerPosition.Y + aMove);

            Texture2D aCollisionCheck = CreateCollisionTexture(aXPosition, aYPosition);



            //Use GetData to fill in an array with all of the Colors of the Pixels in the area of the Collision Texture

            int aPixels = playerBlue.Width * playerBlue.Height;

            Color[] myColors = new Color[aPixels];

            aCollisionCheck.GetData<Color>(0, new Rectangle((int)(aCollisionCheck.Width / 2 - playerBlue.Width / 2),

                (int)(aCollisionCheck.Height / 2 - playerBlue.Height / 2), playerBlue.Width, playerBlue.Height), myColors, 0, aPixels);



            //Cycle through all of the colors in the Array and see if any of them

            //are not Gray. If one of them isn't Gray, then the Car is heading off the road

            //and a Collision has occurred

            bool aCollision = false;

            foreach (Color aColor in myColors)
            {

                //If one of the pixels in that area is not Gray, then the sprite is moving

                //off the allowed movement area

                if (aColor != Color.Gray)
                {

                    aCollision = true;

                    break;

                }

            }



            return aCollision;

        }

        //Create the Collision Texture that contains the rotated Track image for determing

        //the pixels beneath the Car srite.

        private Texture2D CreateCollisionTexture(float theXPosition, float theYPosition)
        {

            //Grab a square of the Track image that is around the Car

            graphics.GraphicsDevice.SetRenderTarget(0, trackRender);

            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);



            spriteBatch.Begin();

            spriteBatch.Draw(grayTrack, new Rectangle(0, 0, playerBlue.Width + 100, playerBlue.Height + 100),

                new Rectangle((int)(theXPosition - 50),

                (int)(theYPosition - 50), playerBlue.Width + 100, playerBlue.Height + 100), Color.White);

            spriteBatch.End();



            graphics.GraphicsDevice.ResolveRenderTarget(0);

            graphics.GraphicsDevice.SetRenderTarget(0, null);



            Texture2D aPicture = trackRender.GetTexture();





            //Rotate the snapshot of the area Around the car sprite and return that 

            graphics.GraphicsDevice.SetRenderTarget(0, trackRender);

            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);



            spriteBatch.Begin();

            spriteBatch.Draw(aPicture, new Rectangle((int)(aPicture.Width / 2), (int)(aPicture.Height / 2),

                aPicture.Width, aPicture.Height), new Rectangle(0, 0, aPicture.Width, aPicture.Width),

                Color.White, 0, new Vector2((int)(aPicture.Width / 2), (int)(aPicture.Height / 2)),

                SpriteEffects.None, 0);

            spriteBatch.End();



            graphics.GraphicsDevice.ResolveRenderTarget(0);

            graphics.GraphicsDevice.SetRenderTarget(0, null);



            return trackRender.GetTexture();

        }
        */



    }
}

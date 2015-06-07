using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoL
{
    class StockScreen:GameScreen
    {
        #region Fields
        string msg;
        Texture2D gradient;
        #endregion

        #region Event Handlers
        public event EventHandler<PlayerIndexEventArgs> StockOne;
        public event EventHandler<PlayerIndexEventArgs> StockTwo;
        public event EventHandler<PlayerIndexEventArgs> StockThree;
        public event EventHandler<PlayerIndexEventArgs> StockFour;
        public event EventHandler<PlayerIndexEventArgs> StockFive;
        public event EventHandler<PlayerIndexEventArgs> StockSix;
        public event EventHandler<PlayerIndexEventArgs> StockSeven;
        public event EventHandler<PlayerIndexEventArgs> StockEight;
        public event EventHandler<PlayerIndexEventArgs> StockNine;
        public event EventHandler<PlayerIndexEventArgs> StockTen;
        #endregion

        #region Initializers
        public StockScreen(string message)
            : this(message, true)
        {
        }

        public StockScreen(string message, bool includeUsageText)
        {
            const string howToUse = "\n1 - Buy stock one \n2 - Buy stock two \n3 - Buy stock three"
                + " \n4 - Buy stock four \n5 - Buy stock five \n6 - Buy stock six \n7 - Buy stock seven"
                + " \n8 - Buy stock eight \n9 - Buy stock nine \n0 - Buy stock ten";
            if (includeUsageText)
            {
                msg = message + howToUse;
            }
            else
            {
                msg = message;
            }
            IsPopup = true;
            TransitioningOnTime = TimeSpan.FromSeconds(0.2);
            TransitioningOffTime = TimeSpan.FromSeconds(0.2);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            gradient = content.Load<Texture2D>("gradient");
        }
        #endregion

        #region Input Handlers
        public override void HandleInput(InputState input)
        {
            PlayerIndex playerIndex;//Perhaps changing from isMenuSelect to 
            //something else in InputState so we can do Y/N message boxes.
            if (input.isNewKeyPress(Keys.D1, ControllingPlayer, out playerIndex))
            {
                if (StockOne != null)
                {
                    StockOne(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D2, ControllingPlayer, out playerIndex))
            {
                if (StockTwo != null)
                {
                    StockTwo(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D3, ControllingPlayer, out playerIndex))
            {
                if (StockThree != null)
                {
                    StockThree(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D4, ControllingPlayer, out playerIndex))
            {
                if (StockFour != null)
                {
                    StockFour(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D5, ControllingPlayer, out playerIndex))
            {
                if (StockFive != null)
                {
                    StockFive(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D6, ControllingPlayer, out playerIndex))
            {
                if (StockSix != null)
                {
                    StockSix(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D7, ControllingPlayer, out playerIndex))
            {
                if (StockSeven != null)
                {
                    StockSeven(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D8, ControllingPlayer, out playerIndex))
            {
                if (StockEight != null)
                {
                    StockEight(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D9, ControllingPlayer, out playerIndex))
            {
                if (StockNine != null)
                {
                    StockNine(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D0, ControllingPlayer, out playerIndex))
            {
                if (StockTen != null)
                {
                    StockTen(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
        }
        #endregion

        #region Draw
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sBatch = ScreenManager.SpriteBatch;
            SpriteFont sFont = ScreenManager.SpriteFont;

            //Darken any screens below the popup message.
            ScreenManager.FadeToBlack(TransitionAlpha * 2 / 3);

            //Center text in viewport
            Viewport vw = ScreenManager.GraphicsDevice.Viewport;
            Vector2 viewportSize = new Vector2(vw.Width, vw.Height);
            Vector2 textSize = sFont.MeasureString(msg);
            Vector2 textPos = (viewportSize - textSize) / 2;
            const int horizontalPadding = 32;
            const int verticalPadding = 16;

            Rectangle backgroundRectangle = new Rectangle((int)textPos.X - horizontalPadding,
                (int)textPos.Y - verticalPadding, (int)textSize.X + horizontalPadding * 2,
                (int)textSize.Y + verticalPadding * 2);

            //Fade popup alpha during transitions.
            Color c = new Color(255, 255, 255, TransitionAlpha);
            sBatch.Begin();
            //Draw bg rectangle.
            sBatch.Draw(gradient, backgroundRectangle, c);
            //Draw message box text.
            sBatch.DrawString(sFont, msg, textPos, c);
            sBatch.End();
        }
        #endregion
    }
}

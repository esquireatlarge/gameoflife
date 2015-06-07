using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoL
{
    class LoseStockScreen:GameScreen
    {
        #region Field
        string msg;
        Texture2D gradient;
        #endregion

        #region Event Handlers
        public event EventHandler<PlayerIndexEventArgs> LoseStockOne;
        public event EventHandler<PlayerIndexEventArgs> LoseStockTwo;
        public event EventHandler<PlayerIndexEventArgs> LoseStockThree;
        public event EventHandler<PlayerIndexEventArgs> LoseStockFour;
        public event EventHandler<PlayerIndexEventArgs> LoseStockFive;
        public event EventHandler<PlayerIndexEventArgs> LoseStockSix;
        public event EventHandler<PlayerIndexEventArgs> LoseStockSeven;
        public event EventHandler<PlayerIndexEventArgs> LoseStockEight;
        public event EventHandler<PlayerIndexEventArgs> LoseStockNine;
        public event EventHandler<PlayerIndexEventArgs> LoseStockTen;
        #endregion

        
        #region Initializers
        public LoseStockScreen(string message)
            : this(message, true)
        {
        }

        public LoseStockScreen(string message, bool includeUsageText)
        {
            const string howToUse = "Hit the number key corresponding to the "
                + "\nnumber of the stock you want to lose";
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
                if (LoseStockOne != null)
                {
                    LoseStockOne(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D2, ControllingPlayer, out playerIndex))
            {
                if (LoseStockTwo != null)
                {
                    LoseStockTwo(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D3, ControllingPlayer, out playerIndex))
            {
                if (LoseStockThree != null)
                {
                    LoseStockThree(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D4, ControllingPlayer, out playerIndex))
            {
                if (LoseStockFour != null)
                {
                    LoseStockFour(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D5, ControllingPlayer, out playerIndex))
            {
                if (LoseStockFive != null)
                {
                    LoseStockFive(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D6, ControllingPlayer, out playerIndex))
            {
                if (LoseStockSix != null)
                {
                    LoseStockSix(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D7, ControllingPlayer, out playerIndex))
            {
                if (LoseStockSeven != null)
                {
                    LoseStockSeven(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D8, ControllingPlayer, out playerIndex))
            {
                if (LoseStockEight != null)
                {
                    LoseStockEight(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D9, ControllingPlayer, out playerIndex))
            {
                if (LoseStockNine != null)
                {
                    LoseStockNine(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D0, ControllingPlayer, out playerIndex))
            {
                if (LoseStockTen != null)
                {
                    LoseStockTen(this, new PlayerIndexEventArgs(playerIndex));
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

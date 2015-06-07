using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoL
{
    class SelectLeftRightScreen:GameScreen
    {
        #region Fields
        string msg;
        Texture2D gradient;
        #endregion

        #region Event Handlers
        public event EventHandler<PlayerIndexEventArgs> RightSelected;
        public event EventHandler<PlayerIndexEventArgs> LeftSelected;
        #endregion

        #region Initializers
        public SelectLeftRightScreen(string message)
            : this(message, true)
        {
        }

        public SelectLeftRightScreen(string message, bool includeUsageText)
        {
            const string howToUse = "\n1 - To the left! \n2 - To the right!";
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
                if (LeftSelected != null)
                {
                    LeftSelected(this, new PlayerIndexEventArgs(playerIndex));
                    //Player loses 40,000 dollaroos here.
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D2, ControllingPlayer, out playerIndex))
            {
                if (RightSelected != null)
                {
                    RightSelected(this, new PlayerIndexEventArgs(playerIndex));
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

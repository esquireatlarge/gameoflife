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
    class TradeSalaryScreen:GameScreen
    {
        string msg;
        Texture2D gradient;

        #region Event Handlers
        public event EventHandler<PlayerIndexEventArgs> FirstPlayerSalary;
        public event EventHandler<PlayerIndexEventArgs> SecondPlayerSalary;
        public event EventHandler<PlayerIndexEventArgs> ThirdPlayerSalary;
        public event EventHandler<PlayerIndexEventArgs> FourthPlayerSalary;
        public event EventHandler<PlayerIndexEventArgs> FifthPlayerSalary;
        public event EventHandler<PlayerIndexEventArgs> SixthPlayerSalary;
        public event EventHandler<PlayerIndexEventArgs> SeventhPlayerSalary;
        public event EventHandler<PlayerIndexEventArgs> EighthPlayerSalary;
        #endregion

        #region Initializers
        public TradeSalaryScreen(string message, int n)
            : this(message, n, true)
        {
        }

        public TradeSalaryScreen(string message,int n, bool includeUsageText)
        {
            //string howToUse = "\n\n1 - First player's Salary\n 2 - Second player's salary \n 3 - Third player's salary"
            //    + "\n 4 - Fourth player's salary \n 5 - Fifth player's salary \n 6 - Sixth player's salary \n 7 - Seventh player's salary"
            //    + "\n 8 - Eighth player's salary";
            string howToUse = "\n";
            for (int i = 0; i < n; i++)
            {
                howToUse += (i + 1) + " - " + (i + 1) + " player's salary\n";
            }
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
                if (FirstPlayerSalary != null)
                {
                    FirstPlayerSalary(this, new PlayerIndexEventArgs(playerIndex));
                    //Player loses 40,000 dollaroos here.
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D2, ControllingPlayer, out playerIndex))
            {
                if (SecondPlayerSalary != null)
                {
                    SecondPlayerSalary(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D3, ControllingPlayer, out playerIndex))
            {
                if (ThirdPlayerSalary != null)
                {
                    ThirdPlayerSalary(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D4, ControllingPlayer, out playerIndex))
            {
                if (FourthPlayerSalary != null)
                {
                    FourthPlayerSalary(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D5, ControllingPlayer, out playerIndex))
            {
                if (FifthPlayerSalary != null)
                {
                    FifthPlayerSalary(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D6, ControllingPlayer, out playerIndex))
            {
                if (SixthPlayerSalary != null)
                {
                    SixthPlayerSalary(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D7, ControllingPlayer, out playerIndex))
            {
                if (SeventhPlayerSalary != null)
                {
                    SeventhPlayerSalary(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D8, ControllingPlayer, out playerIndex))
            {
                if (EighthPlayerSalary != null)
                {
                    EighthPlayerSalary(this, new PlayerIndexEventArgs(playerIndex));
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

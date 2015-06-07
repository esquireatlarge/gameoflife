﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoL
{
    class CareerChoiceScreen:GameScreen
    {
        #region Field
        string msg;
        Texture2D gradient;
        #endregion

        #region Event Handlers
        public event EventHandler<PlayerIndexEventArgs> FirstCareer;
        public event EventHandler<PlayerIndexEventArgs> SecondCareer;
        public event EventHandler<PlayerIndexEventArgs> ThirdCareer;
        #endregion

        
        #region Initializers
        public CareerChoiceScreen(string message)
            : this(message, true)
        {
        }

        public CareerChoiceScreen(string message, bool includeUsageText)
        {
            const string howToUse = "\n1 - First Career\n2-Second Career\n3-Third Career";
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
                if (FirstCareer != null)
                {
                    FirstCareer(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D2, ControllingPlayer, out playerIndex))
            {
                if (SecondCareer != null)
                {
                    SecondCareer(this, new PlayerIndexEventArgs(playerIndex));
                }
                ScreenExit();
            }
            else if (input.isNewKeyPress(Keys.D3, ControllingPlayer, out playerIndex))
            {
                if (ThirdCareer != null)
                {
                    ThirdCareer(this, new PlayerIndexEventArgs(playerIndex));
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

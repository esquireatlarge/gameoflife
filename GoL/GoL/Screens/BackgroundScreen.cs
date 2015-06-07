
#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace GoL
{
    class BackgroundScreen:GameScreen
    {
        #region Declarations
        ContentManager content;
        Texture2D bg;
        #endregion

        #region Initializers
        public BackgroundScreen()
        {
            TransitioningOnTime = TimeSpan.FromSeconds(0.5);
            TransitioningOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            bg = content.Load<Texture2D>("mainmenubg"); //Loads the background...
        }

        public override void UnloadContent()
        {
            content.Unload();
        }
        #endregion

        #region Update
        public override void Update(GameTime gameTime, bool focus, bool covered)
        {
            //Update the base class, GameScreen.
            base.Update(gameTime, focus, false);
        }

        #endregion

        #region Draw
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sBatch = ScreenManager.SpriteBatch;
            Viewport vw = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullScreen = new Rectangle(0, 0, vw.Width, vw.Height);
            byte fade = TransitionAlpha;

            sBatch.Begin(SpriteBlendMode.None);
            sBatch.Draw(bg, fullScreen, new Color(fade, fade, fade));
            sBatch.End();
        }
        #endregion
    }
}

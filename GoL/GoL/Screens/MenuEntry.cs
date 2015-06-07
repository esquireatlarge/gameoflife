
#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace GoL
{
    //Changed from ElapsedGameTime to TotalGameTime in
    //double time entry.
    class MenuEntry
    {
        #region Declarations
        string text; //The text for this entry.
        float fade;  //Deselection effect.
        #endregion

        #region Properties
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }
        #endregion

        #region Handlers
        //When entry is selected.
        public EventHandler<PlayerIndexEventArgs> Selected;

        protected internal virtual void OnSelectEntry(PlayerIndex player)
        {
            if (Selected != null)
            {
                Selected(this, new PlayerIndexEventArgs(player));
            }
        }
        #endregion

        #region Initializer
        public MenuEntry(string text)
        {
            this.text = text;
        }
        #endregion

        #region Update
        public virtual void Update(MenuScreen screen, bool isSelected,
            GameTime gameTime)
        {
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;
            if (isSelected)
            {
                fade = Math.Min(fade + fadeSpeed, 1);
            }
            else
            {
                //Changed from a + to -, Matt Sguerri April 2nd
                fade = Math.Max(fade - fadeSpeed, 0);
            }
        }
        #endregion

        #region Draw
        public virtual void Draw(MenuScreen menuScreen, Vector2 position,
            bool isSelected, GameTime gameTime)
        {
            Color color = new Color();
            if (isSelected)
                //If the menu entry is selected, it will be this color.
                color = Color.RoyalBlue;
            else
            {
                //If menu entry is not selected, it will be this color.
                color.R = 0;
                color.G = 255;
                color.B = 97;
            }

            //This is for the pulsating menu entry.
            //Changed from ElapsedGameTime to TotalGameTime...Matt Sguerri,
            //Now it lasts a lot longer, it's pretty and cuddly.
            double time = gameTime.TotalGameTime.TotalSeconds;
            float pulsate = (float)Math.Sin(time * 6) + 1;
            float scale = 1 + pulsate * 0.05f * fade;

            color = new Color(color.R, color.G, color.B, menuScreen.TransitionAlpha);

            //Draw text.
            ScreenManager screenManager = menuScreen.ScreenManager;
            SpriteBatch sBatch = screenManager.SpriteBatch;
            SpriteFont sFont = screenManager.SpriteFont;
            Vector2 origin = new Vector2(0, sFont.LineSpacing / 2);
            sBatch.DrawString(sFont, text, position, color, 0,
                origin, scale, SpriteEffects.None, 0);
        }
        #endregion

        #region Public Methods
        public virtual int getHeight(MenuScreen screen)
        {
            return screen.ScreenManager.SpriteFont.LineSpacing;
        }

        #endregion
    }
}

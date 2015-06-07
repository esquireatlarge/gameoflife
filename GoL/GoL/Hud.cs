
#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace GoL
{
    class Hud
    {
        //Could this perhaps, also be a screen? o.o
        #region Declarations (or Fields, whatever)
        SpriteBatch sBatch; //The batch...and stuff.
        SpriteFont sFont;
        Color col;
        Color fontCol;
        //This holds the result of the spin.
        //Currently, idea is from a sprite sheet of numbers.
        private Texture2D spinResult; //This holds numeric values representing results
        //of spinning the wheel.
        private static Texture2D panel; //This holds the HUD background
        private static int currPlayer; //Stores the current player.
        #endregion

        #region Properties
        public static Texture2D Texture
        {
            get
            {
                return panel;
            }
            set
            {
                panel = value;
            }
        }

        public Color Color
        {
            get
            {
                return col;
            }
            set
            {
                col = value;
            }
        }

        public SpriteFont Font
        {
            get
            {
                return sFont;
            }
            set
            {
                sFont = value;
            }
        }
        #endregion

        #region Initializer
        public Hud(GraphicsDevice g, Texture2D p, SpriteFont sf)
        {
            //This color affects the color of the hud screen!
            col = new Color(Color.AliceBlue, 1.0f);
            sBatch = new SpriteBatch(g);
            sFont = sf;
            panel = p;
            fontCol = Color.Black;
        }
        #endregion

        #region Update
        public void Update(/*some type of player element*/)
        {
            //Code to update player number, and assets of said player.
            //The player number should be ++ of index.
        }

        #endregion

        #region Draw

        public void Draw(string text)
        {
            sBatch.Begin();
            sBatch.DrawString(sFont, text, new Vector2(5.0f, 930.0f), Color.Red);
            sBatch.End();
        }
        public void Draw(Player[] array, int num)
        {
            string position;
            string balance;
            string spun;
            string tile;
            string retired;
            string salary;
            Player p;
            sBatch.Begin();
            sBatch.Draw(panel, new Rectangle(0, 920, panel.Width, panel.Height), Color.White);
            for (int i = 0; i < num; i++)
            {
                p = array[i];
                position = "Player " + (i + 1) + " position: (" + (500.0f - p.Position.X) + "," + (550.0f - p.Position.Y) + ")";
                balance = "Player " + (i + 1) + " balance: " + p.Balance;
                spun = "Player " + (i + 1) + " spun: " + p.CurrentSpin;
                tile = "Player " + (i + 1) + " at tile: " + p.CurrentTile;
                retired = "Player " + (i + 1) + " has retired!";
                salary = "Player " + (i + 1) + " salary: " + p.Career.Salary.Sal;

                sBatch.DrawString(sFont, position, new Vector2(5.0f + 435.0f*i, 930.0f), fontCol);
                sBatch.DrawString(sFont, balance, new Vector2(5.0f + 435.0f * i, 955.0f), fontCol);
                sBatch.DrawString(sFont, spun, new Vector2(5.0f + 435.0f * i, 980.0f), fontCol);
                sBatch.DrawString(sFont, tile, new Vector2(5.0f + 435.0f * i, 1005.0f), fontCol);
                sBatch.DrawString(sFont, salary, new Vector2(5.0f + 435.0f * i, 900.0f), fontCol);
                if (p.IsRetired)
                    sBatch.DrawString(sFont, retired, new Vector2(230.0f, 930.0f + 25.0f*i), fontCol);
            }
            sBatch.End();
            
        }
        #endregion
    }
}

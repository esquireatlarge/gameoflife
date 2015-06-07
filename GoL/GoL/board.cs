
#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace GoL
{
    class Board
    {
        #region Declarations
        private int playerCount; //Counts the total amount of players on the board.
        private int tileCount; //Number of tiles the board has.
        private Texture2D boardImage; //The image of the board.
        private SpriteBatch sBatch; //The sprite batch for the draw method.
        private Color col; //The color, though this won't appear much, if at all.
        private float boardAlpha;  //In case we want the transparency of the board.
        private Vector2 boardCenter; //Center of the board.
        private float boardRotation; //This is the board rotation, to 
        //coincide with camera rotation.
        private float boardZoom; //Zoom value to use in scale of draw.
        private Vector2 cameraPosition; //Position for the camera.
        #endregion

        #region Properties
        public Texture2D BoardImage
        {
            get
            {
                return boardImage;
            }
            set
            {
                boardImage = value;
            }
        }

        public int PlayerCount
        {
            get
            {
                return playerCount;
            }
            set
            {
                playerCount = value;
            }
        }

        public int TileCount
        {
            get
            {
                return tileCount;
            }
            //We don't want the tile amount to
            //ever be changed.
        }

        public Color BoardColorOverlay
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

        public Vector2 BoardCenter
        {
            get
            {
                return boardCenter;
            }
            //No set, done when image is set.
        }

        /// <summary>
        /// The board alpha controls the transparency of the board.
        /// It is a byte value ranging from 0.0 to 1.0.
        /// </summary>
        public float BoardAlpha
        {
            get
            {
                return boardAlpha;
            }
            set
            {
                if (value < 0.0 || value > 1.0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                boardAlpha = value;
            }
        }

        public float BoardRotation
        {
            get
            {
                return boardRotation;
            }
            set
            {
                boardRotation = value;
            }
        }

        public float BoardZoom
        {
            get
            {
                return boardZoom;
            }
            set
            {
                //error checking
                boardZoom = value;
            }
        }

        public Vector2 CameraPosition
        {
            get
            {
                return cameraPosition;
            }
            set
            {
                cameraPosition = value;
            }
        }

        public int Height
        {
            get
            {
                return boardImage.Height;
            }
        }

        public int Width
        {
            get
            {
                return boardImage.Width;
            }
        }
        #endregion

        #region Initializer
        public Board(GraphicsDevice g, Texture2D image, int tileAmount)
        {
            //Right now, this is the default color.
            col = new Color(32, 94, 234, 1.0f);
            sBatch = new SpriteBatch(g);
            boardImage = image;
            boardZoom = 1.0f;
            boardCenter.X = (boardImage.Width*boardZoom) / 2;
            boardCenter.Y = (boardImage.Height*boardZoom) / 2;
            tileCount = tileAmount;
        }
        #endregion

        #region Update
        public void Update()
        {
        }
        #endregion

        #region Draw
        public void Draw(Vector2 position)
        {
            sBatch.Begin();
            sBatch.Draw(boardImage, position, null, col,BoardRotation, BoardCenter,BoardZoom, SpriteEffects.None, 0);
            sBatch.End();
        }
        #endregion

        #region Public Functions
        #endregion
    }
}

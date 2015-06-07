
#region Class Description
//This class represents the camera that overlooks the board.
//Currently, I do not like the way I did it, but I wasn't sure how
//to do it using a transformation matrix, so I just did it like this.
//It works, but it's really just thrown-together...
#endregion
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion


namespace GoL
{
    class Camera
    {
        #region Declarations
        private Vector2 position;
        private float zoom;
        private float rotation;
        private bool hasCameraChanged;
        private bool isMovingUsingScreenAxis;
        #endregion

        #region Properties
        public Vector2 Position
        {
            set
            {
                if (position != value)
                {
                    hasCameraChanged = true;
                    position = value;
                }
            }
            get
            {
                return position;
            }
        }

        public float Rotation
        {
            set
            {
                if (rotation != value)
                {
                    hasCameraChanged = true;
                    rotation = value;
                }
            }
            get
            {
                return rotation;
            }
        }

        public float Zoom
        {
            set
            {
                if (zoom != value)
                {
                    hasCameraChanged = true;
                    zoom = value;
                }
            }
            get
            {
                return zoom;
            }
        }

        public bool HasChanged
        {
            get
            {
                return hasCameraChanged;
            }
        }

        public bool IsMovingUsingScreenAxis
        {
            set
            {
                isMovingUsingScreenAxis = value;
            }
            get
            {
                return isMovingUsingScreenAxis;
            }
        }
        #endregion

        #region Constructor
        public Camera()
        {
            rotation = 0.0f;
            zoom = 0.0f;
            position = Vector2.Zero;
        }
        #endregion

        #region Movement
        public void reset()
        {
            hasCameraChanged = false;
        }

        public void moveRight(ref float distance)
        {
            if (distance != 0)
            {
                hasCameraChanged = true;
                if (isMovingUsingScreenAxis)
                {
                    position.X += (float)Math.Cos(-rotation) * distance;
                    position.Y += (float)Math.Sin(-rotation) * distance;
                }
                else
                {
                    position.X += distance;
                }
            }
        }

        public void moveLeft(ref float distance)
        {
            if (distance != 0)
            {
                hasCameraChanged = true;
                if (isMovingUsingScreenAxis)
                {
                    position.X -= (float)Math.Cos(-rotation) * distance;
                    position.Y -= (float)Math.Sin(-rotation) * distance;
                }
                else
                {
                    position.X += distance;
                }
            }
        }

        public void moveUp(ref float distance)
        {
            if (distance != 0)
            {
                hasCameraChanged = true;
                if (isMovingUsingScreenAxis)
                {
                    position.X -= (float)Math.Sin(rotation) * distance;
                    position.Y -= (float)Math.Cos(rotation) * distance;
                }
                else
                {
                    position.Y -= distance;
                }
            }
        }

        public void moveDown(ref float distance)
        {
            if (distance != 0)
            {
                hasCameraChanged = true;
                if (isMovingUsingScreenAxis)
                {
                    position.X += (float)Math.Sin(rotation) * distance;
                    position.Y += (float)Math.Cos(rotation) * distance;
                }
                else
                {
                    position.Y -= distance;
                }
            }
        }
        #endregion
    }
}

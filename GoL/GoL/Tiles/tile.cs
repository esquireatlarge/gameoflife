
#region Class Description
//This class represents a tile on the board.
//Each tile has an X,Y position.  If pieces teleported,
//it would be a matter of drawing a player just on the X,Y 
//coordinates of the target tile.  However, if pieces move across
//the board,a for loop which moves across all tiles from 
//the one the player is on to the target tile would work.
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
#endregion

namespace GoL
{
    #region General Comments
    //Anything inheriting from Tile will have:
    //position
    //isOccupied
    //number
    #endregion
    class Tile
    {
        #region Declarations
        Vector2 position;
        bool isOccupied; //Is a player currently on this tile?
        int number; //Which tile is this on the board?
        #endregion

        #region Properties
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        public bool IsOccupied
        {
            get
            {
                return isOccupied;
            }
            set
            {
                isOccupied = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                //In case we assign after initialization.
                position = value;
            }
        }
        #endregion

        #region Initializer
        protected Tile()
        {
            isOccupied = false;
            Position = position;
        }
        #endregion

        #region Public Methods
        public virtual void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
        }
        #endregion
    }
}

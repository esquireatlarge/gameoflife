using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GoL
{
    class LoseTurnTile:Tile
    {
        public LoseTurnTile(Vector2 position)
        {
            Position = position;
        }

        public override void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
            pArray[i].LostTurn = true;
        }
    }
}

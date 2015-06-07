using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GoL
{
    class SpinAgainTile:Tile
    {
        public SpinAgainTile(Vector2 position)
        {
            Position = position;
        }

        public override void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
            int largestTile = 0;
            for (int j = 0; j < amount; j++)
            {
                if (pArray[j].CurrentTile >= largestTile)
                    largestTile = pArray[j].CurrentTile;
            }
            if (largestTile != pArray[i].CurrentTile)
            {
                pArray[i].SpinWheel(); //Spin again.
            }
        }
    }
}

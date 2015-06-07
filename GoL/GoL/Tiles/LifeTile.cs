using System;
using Microsoft.Xna.Framework;

namespace GoL
{
    class LifeTile:Tile
    {
        public LifeTile(Vector2 position)
        {
            Position = position;
        }

        public override void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
            gainLifeCard(ref pArray[i]);
        }

        public void gainLifeCard(ref Player p)
        {
            p.addLifeCard();
        }
    }
}

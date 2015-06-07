using System;
using Microsoft.Xna.Framework;

namespace GoL
{
    class BabySpace:LifeTile
    {
        int amount;

        public int Amount
        {
            get
            {
                return amount;
            }
        }

        public BabySpace(int amt, Vector2 position)
            : base(position)
        {
            Position = position;
            amount = amt;
        }

        public override void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
            base.performTileAction(ref pArray, i, amount, ref b);
            
            gainBaby(ref pArray[i]);
        }

        public void gainBaby(ref Player p)
        {
            p.ChildAmount++;
        }
    }
}

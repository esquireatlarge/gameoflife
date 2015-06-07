using System;
using Microsoft.Xna.Framework;

namespace GoL
{
    class GainMoneySpace:Tile
    {
        int amount;

        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
            }
        }

        public GainMoneySpace(int amt, Vector2 position)
        {
            Position = position;
            Amount = amt;
        }

        public override void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
            if (Number == 145) //This is the one where you gain times your spin number
            {
                Amount *= pArray[i].CurrentSpin;
            }
            gainMoney(ref pArray[i],ref b);
        }

        public void gainMoney(ref Player p, ref Bank b)
        {
            p.Balance += Amount;
            b.AmountDispensed -= Amount;
        }
    }
}

using System;
using Microsoft.Xna.Framework;

namespace GoL
{
    class LoseMoneySpace:Tile
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

        public LoseMoneySpace(int amt, Vector2 position)
        {
            Position = position;
            Amount = amt;
        }

        public override void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
            loseMoney(ref pArray[i], ref b);
        }

        public void loseMoney(ref Player p, ref Bank b)
        {
            if (p.Balance < Amount)
            {
                p.AmountOwed += (Amount - p.Balance);
                b.AmountLoaned += (Amount - p.Balance);
            }
            p.Balance -= Amount;
            b.AmountReceived += Amount;
            /*
            if(Number == 29) //This is a career lose money tile.
                for(int i = 0; i < ; i++)
                    if plArray[i].Career.Name == "Teacher"
                        plArray[i].Balance += 10000;*/
        }
    }
}

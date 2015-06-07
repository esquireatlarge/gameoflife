using System;
using Microsoft.Xna.Framework;

namespace GoL
{
    class RetireSpace:Tile
    {
        bool protection;

        public bool Protection
        {
            get
            {
                return protection;
            }
            set
            {
                protection = value;
            }
        }

        public RetireSpace(Vector2 position)
        {
            Position = position;
        }

        public override void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
            playerRetire(ref pArray[i], ref b);
        }

        private void playerRetire(ref Player p, ref Bank b)
        {
            p.IsRetired = true; //The player has retired.
            //Here is where we figure out if we really need derived 
            //classes for the type of retirement.

            //Calculated the grand total of the player.
            p.GrandTotal += p.Balance;
            //Get amount from life cards
            for (int i = 0; i < p.LifeCardCount; i++)
            {
                p.GrandTotal += (p.LifeCards[i].Amount);
            }

            //Make sure all loans are paid back in full.
            p.GrandTotal -= p.AmountOwed;
            //ADD TO BANK
        }
    }
}

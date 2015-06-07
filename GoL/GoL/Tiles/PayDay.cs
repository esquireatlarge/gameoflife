using System;
using Microsoft.Xna.Framework;

namespace GoL
{
    class PayDayTile:Tile
    {
        public PayDayTile(Vector2 position)
        {
            Position = position;
        }

        public override void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
            getPaid(ref pArray[i], ref b);
        }

        private void getPaid(ref Player p, ref Bank b)
        {
            p.Balance += p.Career.Salary.Sal;
            b.dispenseSalary(ref p);
        }
    }
}

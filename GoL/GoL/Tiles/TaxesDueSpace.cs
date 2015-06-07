using System;
using Microsoft.Xna.Framework;

namespace GoL
{
    class TaxesDueSpace:Tile
    {
        public TaxesDueSpace(Vector2 position)
        {
            Position = position;
        }

        public override void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
            payTaxes(ref pArray[i], ref b);
        }

        public void payTaxes(ref Player p, ref Bank b)
        {
            p.Balance -= p.Career.Tax;
            b.receiveTax(p.Career.Tax);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GoL
{
    class Stock
    {
        int number; 
        //The number that if you (or other players) spin, you get paid.

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

        #region Initializer
        public Stock(int i)
        {
            number = i;
            //If the owner of the stock is -1, it means
            //no one owns it.
        }

        public Stock(Stock s)
        {
            number = s.Number;
        }
        #endregion
    }
}

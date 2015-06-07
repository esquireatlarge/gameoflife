
#region Class Description
//This class represents one of the life cards the player can get.
#endregion
#region Using Statements
using System;
using Microsoft.Xna.Framework;

#endregion

namespace GoL
{
    class LifeCard
    {
        #region Declarations
        int amount;
        Random r = new Random();
        #endregion

        #region Properties
        public int Amount
        {
            get
            {
                return amount;
            }
        }
        #endregion

        #region Initializer
        public LifeCard()
        {
            //Determine a random amount between 20,000 and 100,000.
            amount = r.Next(20000, 100000);
        }

        public LifeCard(LifeCard l)
        {
            amount = l.amount;
        }
        #endregion
    }
}

using System;
using Microsoft.Xna.Framework;

namespace GoL
{
    class Bank
    {
        int amountLoaned;
        int amountReceived;
        int taxGained;
        int amountDispensed; //When competitions are won?

        #region Properties
        public int AmountLoaned
        {
            get
            {
                return amountLoaned;
            }
            set
            {
                amountLoaned = value;
            }
        }

        public int AmountReceived
        {
            get
            {
                return amountReceived;
            }
            set
            {
                amountReceived = value;
            }
        }

        public int TaxGained
        {
            get
            {
                return taxGained;
            }
            set
            {
                taxGained = value;
            }
        }

        public int AmountDispensed
        {
            get
            {
                return amountDispensed;
            }
            set
            {
                amountDispensed = value;
            }
        }
        #endregion

        #region Initializer
        public Bank()
        {
            AmountDispensed = 0;
            AmountLoaned = 0;
            TaxGained = 0;
            AmountReceived = 0;
        }

        public Bank(Bank b)
        {
            AmountDispensed = b.AmountDispensed;
            AmountLoaned = b.AmountLoaned;
            TaxGained = b.TaxGained;
            AmountReceived = b.AmountReceived;
        }
        #endregion

        #region Public Methods
        public void receiveTax(int amount)
        {
            TaxGained += amount;
        }

        public void dispenseSalary(ref Player p)
        {
            AmountDispensed += p.Salary.Sal;
        }
        #endregion
    }
}

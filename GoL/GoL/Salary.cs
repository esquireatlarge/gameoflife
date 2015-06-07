using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoL
{
    class Salary
    {
        int salary;
        string color;

        #region Properties
        public int Sal
        {
            get
            {
                return salary;
            }
            set
            {
                salary = value;
            }
        }

        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
        #endregion

        #region Initializer
        public Salary(int sal, string col)
        {
            Color = col;
            Sal = sal;
        }

        public Salary()
        {
            Color = "None";
            Sal = 0;
        }

        public Salary(Salary s)
        {
            Color = s.Color;
            Sal = s.Sal;
        }
        #endregion
    }
}

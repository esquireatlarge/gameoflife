using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoL
{
    class CareerCard
    {
        string name;
        bool degreeRequired;
        int tax;
        Salary salary;
        Salary s1, s2, s3; //Choices of salary for this career.
        ScreenManager sM;
        string color; //Not implemented as an actual color!

        #region Properties
        public string Name
        {
            get
            {
                return name;
            }
        }

        public bool DegreeRequired
        {
            get
            {
                return degreeRequired;
            }
        }

        public int Tax
        {
            get
            {
                return tax;
            }
        }

        public Salary Salary
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
        }

        public ScreenManager SM
        {
            get
            {
                return sM;
            }
        }
        #endregion

        #region Initializer
        public CareerCard(string nm, bool degree, int tax, string col, ScreenManager s)
        {
            name = nm;
            degreeRequired = degree;
            this.tax = tax;
            color = col;
            salary = new Salary(0, "Green");
            sM = s;
        }

        public CareerCard()
        {
            name = "Default";
            degreeRequired = false;
            this.tax = 0;
            color = "None";
            salary = new Salary();
            sM = null;
        }

        public CareerCard(CareerCard cc)
        {
            name = cc.name;
            degreeRequired = cc.degreeRequired;
            this.tax = cc.Tax;
            color = cc.Color;
            salary = cc.Salary;
            sM = cc.SM;
        }
        #endregion

        #region Public Methods
        public void selectSalary(int maxVal)
        {
            Random r = new Random();
            s1 = new Salary(r.Next(20000, maxVal), "Red");
            s2 = new Salary(r.Next(20000, maxVal), "Green");
            s3 = new Salary(r.Next(20000, maxVal), "Blue");
            string message = "1.Salary: " + s1.Sal +
                " \n2.Salary: " + s2.Sal +
                " \n3.Salary: " + s3.Sal;
            SelectSalaryScreen selectSalary = new SelectSalaryScreen(message);
            selectSalary.FirstSalary += FirstSalarySelected;
            selectSalary.SecondSalary += SecondSalarySelected;
            selectSalary.ThirdSalary += ThirdSalarySelected;

            sM.AddScreen(selectSalary, null);
        }
        #endregion

        #region Methods
        void FirstSalarySelected(object sender, PlayerIndexEventArgs e)
        {
            //Set the player's salary to the first one selected.
            Salary = new Salary(s1);
        }
        void SecondSalarySelected(object sender, PlayerIndexEventArgs e)
        {
            Salary = new Salary(s2);
        }
        void ThirdSalarySelected(object sender, PlayerIndexEventArgs e)
        {
            Salary = new Salary(s3);
        }
        #endregion
    }
}

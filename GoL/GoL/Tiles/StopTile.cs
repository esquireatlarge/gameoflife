#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
#endregion

namespace GoL
{
    class StopTile:Tile
    {
        #region Fields
        private int whichStopSign;
        CareerChoiceScreen ccs = new CareerChoiceScreen("Select from these 3 careers!");
        PurchaseHouseScreen phs;
        ScreenManager sM;
        int whichPlayer;
        Player[] tempPlayerArray;
        CareerCard career1;
        CareerCard career2;
        CareerCard career3;
        MarriageScreen ms;
        Bank tempBank;
        #endregion

        #region Properties (Accessors)
        public int KindOfStopSign
        {
            get
            {
                return whichStopSign;
            }
            set
            {
                whichStopSign = value;
            }
        }
        #endregion

        #region Initializer
        /// <summary>
        /// The constructor for the Stop Tile.
        /// </summary>
        /// <param name="whichStopSign">
        /// There are different kinds of stop signs.
        /// These include get married, buy house, career, etc.
        /// 1:  Career choice.
        /// 2:  Get married.
        /// 3:  Buy house.
        /// </param>
        /// <param name="position">
        /// Where is the tile located on the board?  Float values.
        /// </param>
        public StopTile(int whichStopSign, Vector2 position, ScreenManager s)
        {
            this.whichStopSign = whichStopSign;
            Position = position;
            ccs.FirstCareer += FirstCareerSelected;
            ccs.SecondCareer += SecondCareerSelected;
            ccs.ThirdCareer += ThirdCareerSelected;
            sM = s;
            tempPlayerArray = new Player[8];
            career1 = new CareerCard("Doctor", true, 35000, "Blue", sM);
            career2 = new CareerCard("Teacher", true, 20000, "Green", sM);
            career3 = new CareerCard("Researcher", true, 10000, "Red", sM);
        }
        #endregion

        /// <summary>
        /// Perform this tile's function.
        /// </summary>
        /// <param name="p">The player responsible for this action.</param>
        public override void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
            whichPlayer = i;
            for (int j = 0; j < amount; j++)
            {
                tempPlayerArray[j] = new Player(pArray[j]);
            }

            if (KindOfStopSign == 1) //This is a career choice tile.
            {
                sM.AddScreen(ccs, null);
                pArray[i] = tempPlayerArray[i];
            }
            else if (KindOfStopSign == 2) //A tile to get married.
            {
                pArray[i].IsMarried = true;
                pArray[i].addLifeCard();
                ms = new MarriageScreen("You got married!  Congratulations!");
                sM.AddScreen(ms, null);
            }
            else //House.
            {
                tempBank = new Bank(b);
                string houseMsg = "Choose from among these three houses!"
                    + "\n1 - Victorian Mansion: $200,000"
                    + "\n2 - Studio Apartment: $15,000\n3 - Townhouse: $80,000";
                phs = new PurchaseHouseScreen(houseMsg);
                phs.FirstHouseSelected += FirstHouseChosen;
                phs.SecondHouseSelected += SecondHouseChosen;
                phs.ThirdHouseSelected += ThirdHouseChosen;
                phs.NoHouseSelected += NoHouseChosen;
                sM.AddScreen(phs, null);
                pArray[i] = tempPlayerArray[i];
                b = new Bank(tempBank);
            }
        }

        private void FirstCareerSelected(object sender, PlayerIndexEventArgs e)
        {
            career1.selectSalary(150000);
            tempPlayerArray[whichPlayer].Career = career1;
            tempPlayerArray[whichPlayer].Career.Salary.Sal = career1.Salary.Sal;
        }

        private void SecondCareerSelected(object sender, PlayerIndexEventArgs e)
        {
            career2.selectSalary(98000);
            tempPlayerArray[whichPlayer].Career = career2;
            tempPlayerArray[whichPlayer].Career.Salary.Sal = career2.Salary.Sal;
        }

        private void ThirdCareerSelected(object sender, PlayerIndexEventArgs e)
        {
            career3.selectSalary(120000);
            tempPlayerArray[whichPlayer].Career = career3;
            tempPlayerArray[whichPlayer].Career.Salary.Sal = career3.Salary.Sal;
        }

        private void FirstHouseChosen(object sender, PlayerIndexEventArgs e)
        {
            tempPlayerArray[whichPlayer].HasHouse = true;
            tempPlayerArray[whichPlayer].Balance -= 200000;
            if (tempPlayerArray[whichPlayer].Balance < 200000)
                tempBank.AmountLoaned += (200000 - tempPlayerArray[whichPlayer].Balance);
            //BANK CODE
        }

        private void SecondHouseChosen(object sender, PlayerIndexEventArgs e)
        {
            tempPlayerArray[whichPlayer].HasHouse = true;
            tempPlayerArray[whichPlayer].Balance -= 15000;
            //BANK CODE
        }

        private void ThirdHouseChosen(object sender, PlayerIndexEventArgs e)
        {
            tempPlayerArray[whichPlayer].HasHouse = true;
            tempPlayerArray[whichPlayer].Balance -= 80000;
            //BANK CODE
        }

        private void NoHouseChosen(object sender, PlayerIndexEventArgs e)
        {
            tempPlayerArray[whichPlayer].HasHouse = false;
            //No bank code.
        }
    }
}

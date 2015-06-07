using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GoL
{
    class TradeSalaryTile : Tile
    {
        private Player[] array;
        private int n;
        private int s;
        ScreenManager sM;

        public TradeSalaryTile(Vector2 position, ScreenManager s)
        {
            array = new Player[8];
            Position = position;
            sM = s;
        }

        public override void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
            s = i;
            //You need to know all the players for this tile, it's unique.
            for (int j = 0; j < amount; j++)
                array[j] = new Player(pArray[j]);
            TradeSalaryScreen tss = new TradeSalaryScreen("Trade your card with another player,\n or keep your own!", amount);
            tss.FirstPlayerSalary += FirstPlayersSalarySelected;
            tss.SecondPlayerSalary += SecondPlayersSalarySelected;
            tss.ThirdPlayerSalary += ThirdPlayersSalarySelected;
            tss.FourthPlayerSalary += FourthPlayersSalarySelected;
            tss.FifthPlayerSalary += FifthPlayersSalarySelected;
            tss.SixthPlayerSalary += SixthPlayersSalarySelected;
            tss.SeventhPlayerSalary += SeventhPlayersSalarySelected;
            tss.EighthPlayerSalary += EighthPlayersSalarySelected;
            sM.AddScreen(tss, null);
            //This is because you cannot pass pArray to the event handlers (not that I know of), 
            //so I had to rely on this method.  Copy back the plaer into pArray[i]!
            pArray[i] = new Player(array[i]);
        }

        #region Event Hookups
        void FirstPlayersSalarySelected(object sender, PlayerIndexEventArgs e)
        {
            Salary temp;
            temp = array[0].Salary;
            array[0].Salary = array[s].Salary;
            array[s].Salary = temp;
        }

        void SecondPlayersSalarySelected(object sender, PlayerIndexEventArgs e)
        {
            Salary temp;
            temp = array[1].Salary;
            array[1].Salary = array[s].Salary;
            array[s].Salary = temp;
        }

        void ThirdPlayersSalarySelected(object sender, PlayerIndexEventArgs e)
        {
            Salary temp;
            temp = array[2].Salary;
            array[2].Salary = array[s].Salary;
            array[s].Salary = temp;
        }

        void FourthPlayersSalarySelected(object sender, PlayerIndexEventArgs e)
        {
            Salary temp;
            temp = array[3].Salary;
            array[3].Salary = array[s].Salary;
            array[s].Salary = temp;
        }

        void FifthPlayersSalarySelected(object sender, PlayerIndexEventArgs e)
        {
            Salary temp;
            temp = array[4].Salary;
            array[4].Salary = array[s].Salary;
            array[s].Salary = temp;
        }

        void SixthPlayersSalarySelected(object sender, PlayerIndexEventArgs e)
        {
            Salary temp;
            temp = array[5].Salary;
            array[5].Salary = array[s].Salary;
            array[s].Salary = temp;
        }

        void SeventhPlayersSalarySelected(object sender, PlayerIndexEventArgs e)
        {
            Salary temp;
            temp = array[6].Salary;
            array[6].Salary = array[s].Salary;
            array[s].Salary = temp;
        }

        void EighthPlayersSalarySelected(object sender, PlayerIndexEventArgs e)
        {
            Salary temp;
            temp = array[7].Salary;
            array[7].Salary = array[s].Salary;
            array[s].Salary = temp;
        }
        #endregion

    }
}

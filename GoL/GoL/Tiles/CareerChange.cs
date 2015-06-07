using System;
using Microsoft.Xna.Framework;

namespace GoL
{
    class CareerChange:Tile
    {
        ScreenManager sM;
        CareerCard career1;
        CareerCard career2;
        CareerCard career3;
        Player tempPlayer;

        public CareerChange(Vector2 position, ScreenManager s)
        {
            Position = position;
            sM = s;
            career1 = new CareerCard("Farmer", false, 10000, "Red", sM);
            career2 = new CareerCard("Priest", true, 0, "Blue", sM);
            career3 = new CareerCard("Professor", true, 23000, "Green", sM);
        }

        public override void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
            changeCareer(ref pArray[i]);
        }

        public void changeCareer(ref Player p)
        {
            string msg = "Select a new career!\n1 - " + career1.Name
                + "\n2 - " + career2.Name
                + "\n3 - " + career3.Name + "";
            CareerChoiceScreen ccs = new CareerChoiceScreen(msg);
            ccs.FirstCareer += FirstCareer;
            ccs.SecondCareer += SecondCareer;
            ccs.ThirdCareer += ThirdCareer;
            tempPlayer = new Player(p);
            sM.AddScreen(ccs, null);
            p = new Player(tempPlayer);
        }

        void FirstCareer(object sender, PlayerIndexEventArgs e)
        {
            career1.selectSalary(60000);
            tempPlayer.Career = new CareerCard(career1);
            tempPlayer.Career.Salary.Sal = career1.Salary.Sal;
        }

        void SecondCareer(object sender, PlayerIndexEventArgs e)
        {
            career2.selectSalary(70000);
            tempPlayer.Career = new CareerCard(career2);
            tempPlayer.Career.Salary.Sal = career2.Salary.Sal;
        }

        void ThirdCareer(object sender, PlayerIndexEventArgs e)
        {
            career3.selectSalary(100000);
            tempPlayer.Career = new CareerCard(career3);
            tempPlayer.Career.Salary.Sal = career3.Salary.Sal;
        }
    }
}

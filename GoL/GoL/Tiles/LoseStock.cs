using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GoL
{
    class LoseStock:Tile
    {
        ScreenManager sM;
        Player temp;
        LoseStockScreen lss;

        public LoseStock(Vector2 position, ScreenManager s)
        {
            Position = position;
            sM = s;
        }

        public override void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
            string msg = "You have the following stock(s): ";
            if (pArray[i].StockCount != 0)
            {
                temp = new Player(pArray[i]);
                for (int j = 0; i < pArray[j].StockCount; j++)
                {
                    if (pArray[i].StockNumberArray[j] == 1)
                    {
                        lss.LoseStockOne += StockOneLost;
                        msg += "1, ";
                    }
                    if (pArray[i].StockNumberArray[j] == 2)
                    {
                        lss.LoseStockTwo += StockTwoLost;
                        msg += "2, ";
                    }
                    if (pArray[i].StockNumberArray[j] == 3)
                    {
                        lss.LoseStockThree += StockThreeLost;
                        msg += "3, ";
                    }
                    if (pArray[i].StockNumberArray[j] == 4)
                    {
                        lss.LoseStockFour += StockFourLost;
                        msg += "4, ";
                    }
                    if (pArray[i].StockNumberArray[j] == 5)
                    {
                        lss.LoseStockFive += StockFiveLost;
                        msg += "5, ";
                    }
                    if (pArray[i].StockNumberArray[j] == 6)
                    {
                        lss.LoseStockSix += StockSixLost;
                        msg += "6, ";
                    }
                    if (pArray[i].StockNumberArray[j] == 7)
                    {
                        lss.LoseStockSeven += StockSevenLost;
                        msg += "7, ";
                    }
                    if (pArray[i].StockNumberArray[j] == 8)
                    {
                        lss.LoseStockEight += StockEightLost;
                        msg += "8, ";
                    }
                    if (pArray[i].StockNumberArray[j] == 9)
                    {
                        lss.LoseStockNine += StockNineLost;
                        msg += "9, ";
                    }
                    if (pArray[i].StockNumberArray[j] == 10)
                    {
                        lss.LoseStockTen += StockTenLost;
                        msg += "10 (hit zero), ";
                    }
                }
                lss = new LoseStockScreen(msg);
                sM.AddScreen(lss, null);
                pArray[i] = new Player(temp);
            }
        }

        void StockOneLost(object sender, PlayerIndexEventArgs e)
        {
            for (int i = 0; i < temp.StockCount; i++)
            {
                if (temp.StockNumberArray[i] == 1)
                    //Make it unused again.
                    temp.StockNumberArray[i] = -1;
            }
        }

        void StockTwoLost(object sender, PlayerIndexEventArgs e)
        {
            for (int i = 0; i < temp.StockCount; i++)
            {
                if (temp.StockNumberArray[i] == 2)
                    //Make it unused again.
                    temp.StockNumberArray[i] = -1;
            }
        }

        void StockThreeLost(object sender, PlayerIndexEventArgs e)
        {
            for (int i = 0; i < temp.StockCount; i++)
            {
                if (temp.StockNumberArray[i] == 3)
                    //Make it unused again.
                    temp.StockNumberArray[i] = -1;
            }
        }

        void StockFourLost(object sender, PlayerIndexEventArgs e)
        {
            for (int i = 0; i < temp.StockCount; i++)
            {
                if (temp.StockNumberArray[i] == 4)
                    //Make it unused again.
                    temp.StockNumberArray[i] = -1;
            }
        }
        void StockFiveLost(object sender, PlayerIndexEventArgs e)
        {
            for (int i = 0; i < temp.StockCount; i++)
            {
                if (temp.StockNumberArray[i] == 5)
                    //Make it unused again.
                    temp.StockNumberArray[i] = -1;
            }
        }
        void StockSixLost(object sender, PlayerIndexEventArgs e)
        {
            for (int i = 0; i < temp.StockCount; i++)
            {
                if (temp.StockNumberArray[i] == 6)
                    //Make it unused again.
                    temp.StockNumberArray[i] = -1;
            }
        }
        void StockSevenLost(object sender, PlayerIndexEventArgs e)
        {
            for (int i = 0; i < temp.StockCount; i++)
            {
                if (temp.StockNumberArray[i] == 7)
                    //Make it unused again.
                    temp.StockNumberArray[i] = -1;
            }
        }
        void StockEightLost(object sender, PlayerIndexEventArgs e)
        {
            for (int i = 0; i < temp.StockCount; i++)
            {
                if (temp.StockNumberArray[i] == 8)
                    //Make it unused again.
                    temp.StockNumberArray[i] = -1;
            }
        }
        void StockNineLost(object sender, PlayerIndexEventArgs e)
        {
            for (int i = 0; i < temp.StockCount; i++)
            {
                if (temp.StockNumberArray[i] == 9)
                    //Make it unused again.
                    temp.StockNumberArray[i] = -1;
            }
        }
        void StockTenLost(object sender, PlayerIndexEventArgs e)
        {
            for (int i = 0; i < temp.StockCount; i++)
            {
                if (temp.StockNumberArray[i] == 10)
                    //Make it unused again.
                    temp.StockNumberArray[i] = -1;
            }
        }
    }
}

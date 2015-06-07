using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GoL
{
    class GainStock:Tile
    {
        Player tempPlayer;
        ScreenManager sM;
        StockScreen ss;

        public GainStock(Vector2 position, ScreenManager s)
        {
            Position = position;
            sM = s;
        }

        public override void performTileAction(ref Player[] pArray, int i, int amount, ref Bank b)
        {
            ss = new StockScreen("Choose a stock number!");
            ss.StockOne += FirstStock;
            ss.StockTwo += SecondStock;
            ss.StockThree += ThirdStock;
            ss.StockFour += FourthStock;
            ss.StockFive += FifthStock;
            ss.StockSix += SixthStock;
            ss.StockSeven += SeventhStock;
            ss.StockEight += EightStock;
            ss.StockNine += NinthStock;
            ss.StockTen += TenthStock;

            tempPlayer = new Player(pArray[i]);
            sM.AddScreen(ss, null);
            pArray[i] = new Player(tempPlayer);
        }

        void FirstStock(object sender, PlayerIndexEventArgs e)
        {
            tempPlayer.addStock(new Stock(1));
        }

        void SecondStock(object sender, PlayerIndexEventArgs e)
        {
            tempPlayer.addStock(new Stock(2));
        }

        void ThirdStock(object sender, PlayerIndexEventArgs e)
        {
            tempPlayer.addStock(new Stock(3));
        }

        void FourthStock(object sender, PlayerIndexEventArgs e)
        {
            tempPlayer.addStock(new Stock(4));
        }

        void FifthStock(object sender, PlayerIndexEventArgs e)
        {
            tempPlayer.addStock(new Stock(5));
        }

        void SixthStock(object sender, PlayerIndexEventArgs e)
        {
            tempPlayer.addStock(new Stock(6));
        }

        void SeventhStock(object sender, PlayerIndexEventArgs e)
        {
            tempPlayer.addStock(new Stock(7));
        }

        void EightStock(object sender, PlayerIndexEventArgs e)
        {
            tempPlayer.addStock(new Stock(8));
        }

        void NinthStock(object sender, PlayerIndexEventArgs e)
        {
            tempPlayer.addStock(new Stock(9));
        }

        void TenthStock(object sender, PlayerIndexEventArgs e)
        {
            tempPlayer.addStock(new Stock(10));
        }
    }
}

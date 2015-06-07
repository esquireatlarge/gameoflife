
#region Using Statements
using System;
using Microsoft.Xna.Framework;
#endregion

namespace GoL
{
    //Frankly, Microsoft's MSDN said this was necessary, so it is here.
    //Do I know what it does?  Not a clue. - Matt.
    class PlayerIndexEventArgs:EventArgs
    {
        public PlayerIndexEventArgs(PlayerIndex player)
        {
            this.player = player;
        }

        public PlayerIndex PlayerIndex
        {
            get
            {
                return player;
            }
        }
        PlayerIndex player;
    }
}

#region Using Statements
using System;
using Microsoft.Xna.Framework;
#endregion

namespace GoL
{
    //This screen appears over the main menu.
    class OptionsMenuScreen:MenuScreen
    {
        #region Declarations

        static int[] playerAmount = {2,3,4,5,6,7,8 };
        static int currAmount = 2;
        private bool teleport = false;
        private string yesNo = "No";

        #endregion

        #region Properties
        public int PlayerAmount
        {
            get
            {
                return currAmount;
            }
            //No set because we do not want currAmount to be overwritten from
            //outside sources.
        }

        public bool TeleportPieces
        {
            get
            {
                return teleport;
            }
        }
        #endregion

        #region Initializer
        public OptionsMenuScreen():base("Options")
        {
            //Make the entry(ies)

            MenuEntry goBackEntry = new MenuEntry("Go Back");

            setMenuEntryText();

            //Hook up event handlers. <---This was kind of like 261..
            goBackEntry.Selected += OnCancel;

            

            //Add entries to menu
            MenuEntries.Add(goBackEntry);
        }

        void setMenuEntryText()
        {
            //Updates the text of the entry, so when you hit it, it
            //shows it updating.
            //playersEntry.Text = "Amount of players: " + currAmount;
            //teleportEntry.Text = "Teleport Pieces? " + yesNo;
        }
        #endregion

        #region Input Handlers
        //Handler for when the player entry is selected.
        void PlayersEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currAmount++; //Increase the selection.
            if (currAmount > 8) currAmount = 2; //Can't be over 8...
            setMenuEntryText();
        }

        //I'm not even sure if we need this method.  I just added it to have
        //more things in the option menu.
        void TeleportEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            teleport = !teleport;
            if (teleport == false)
                yesNo = "No";
            else
                yesNo = "Yes";
            setMenuEntryText();
        }
        #endregion
    }
}

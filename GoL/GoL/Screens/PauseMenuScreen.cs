
#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace GoL
{
    //The screen that shows up when you press the ESC key in-game.
    class PauseMenuScreen:MenuScreen
    {
        #region Initializer
        public PauseMenuScreen()
            : base("Game Paused")
        {
            IsPopup = true;

            //The menu entries the menu will have.
            MenuEntry resumeEntry = new MenuEntry("Resume");
            MenuEntry helpEntry = new MenuEntry("Help");
            MenuEntry quitEntry = new MenuEntry("Quit");
            //Perhaps add a save/load entry.

            //Hook up the entries.
            resumeEntry.Selected += OnCancel;
            helpEntry.Selected += HelpEntry;
            quitEntry.Selected += QuitGameEntry;

            MenuEntries.Add(resumeEntry);
            MenuEntries.Add(helpEntry);
            MenuEntries.Add(quitEntry);
        }
        #endregion

        #region Input Handlers
        void QuitGameEntry(object sender, PlayerIndexEventArgs e)
        {
            const string msg = "Are you certain you wish to quit?";
            MessageBoxScreen confirmQuitMessage = new MessageBoxScreen(msg);
            confirmQuitMessage.Accept += ConfirmQuitAccepted;
            ScreenManager.AddScreen(confirmQuitMessage, ControllingPlayer);
        }

        void ConfirmQuitAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenu());
        }

        void HelpEntry(object sender, PlayerIndexEventArgs e)
        {
            string msg = "Press S to spin, or B to buy!\nPress space to exit this message";
            MessageBoxScreen help = new MessageBoxScreen(msg, false);
            help.Accept += HelpAccept;
            ScreenManager.AddScreen(help, null);
        }

        void HelpAccept(object sender, PlayerIndexEventArgs e)
        {
        }
        #endregion
    }
}

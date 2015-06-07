#region Class Description
/*
 * This class handles input for things like pausing, 
 * or moving upwards and downwards through menus, etc.
 * See, you don't technically even need this for the GoL, 
 * but it does make it nicer.
 * 
 */
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace GoL
{
    //Complete...rechecked.
    public class InputState
    {
        #region Declarations
        public const int MAXINPUT = 4; //Maximum player count, no, it isn't.
        //That's the maximum amount of controllers the Xbox allows.
        public readonly KeyboardState[] currKeyboardStates;

        //This is only for the Xbox version, which wouldn't really
        //be necessary for this particular project.
        //It is only here for future re-use.
        public readonly GamePadState[] currGamepadStates;

        public readonly KeyboardState[] lastKeyboardStates;
        public readonly GamePadState[] lastGamepadStates;
        public readonly MouseState[] currMouseStates;
        public readonly MouseState[] lastMouseStates;

        //For four player Xboxes...
        public readonly bool[] GamepadConnected;

        #endregion

        #region Initializers
        public InputState()
        {
            //Basically just initialize all
            //declarations above.
            currKeyboardStates = new KeyboardState[MAXINPUT];
            currGamepadStates = new GamePadState[MAXINPUT];
            currMouseStates = new MouseState[MAXINPUT];
            lastKeyboardStates = new KeyboardState[MAXINPUT];
            lastGamepadStates = new GamePadState[MAXINPUT];
            lastMouseStates = new MouseState[MAXINPUT];
            GamepadConnected = new bool[MAXINPUT];

        }
        #endregion

        #region Public  Methods
        public void Update()
        {
            for (int i = 0; i < MAXINPUT; i++)
            {
                lastKeyboardStates[i] = currKeyboardStates[i];
                lastGamepadStates[i] = currGamepadStates[i];
                lastMouseStates[i] = currMouseStates[i];
                currKeyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
                currGamepadStates[i] = GamePad.GetState((PlayerIndex)i);
                currMouseStates[i] = Mouse.GetState();
                //This keeps track of whether or not a gamepad was
                //ever even connected.  If it is unplugged, you know.
                if (currGamepadStates[i].IsConnected == true)
                {
                    GamepadConnected[i] = true;
                }
            }
        }

        public bool isNewKeyPress(Keys key, PlayerIndex? controllingPlayer,
            out PlayerIndex player)
        {
            if (controllingPlayer.HasValue)
            {
                //Read input from specified player.
                player = controllingPlayer.Value;
                int i = (int)player;
                return (currKeyboardStates[i].IsKeyDown(key) &&
                    lastKeyboardStates[i].IsKeyUp(key));
            }
            else
            {
                //Accept input from any player.
                return (isNewKeyPress(key, PlayerIndex.One, out player) ||
                    isNewKeyPress(key, PlayerIndex.Two, out player) ||
                    isNewKeyPress(key, PlayerIndex.Three, out player) ||
                    isNewKeyPress(key, PlayerIndex.Four, out player));
            }
        }

        //Gamepad equivalent of above.
        public bool IsNewButtonPress(Buttons button, PlayerIndex? controllingPlayer,
                                                     out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (currGamepadStates[i].IsButtonDown(button) &&
                        lastGamepadStates[i].IsButtonUp(button));
            }
            else
            {
                // Accept input from any player.
                return (IsNewButtonPress(button, PlayerIndex.One, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Two, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Three, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Four, out playerIndex));
            }
        }

        public bool IsNewMousePress(PlayerIndex? controllingPlayer, out PlayerIndex player)
        {
            if (controllingPlayer.HasValue)
            {
                player = controllingPlayer.Value;
                int i = (int)player;
                return currMouseStates[i].LeftButton == ButtonState.Pressed;
            }
            else
            {
                 return (IsNewMousePress(PlayerIndex.Two, out player)) ||
                     (IsNewMousePress(PlayerIndex.Three, out player)) ||
                     (IsNewMousePress(PlayerIndex.Four, out player));
            }
        }

        public bool isMenuSelect(PlayerIndex? controllingPlayer, out PlayerIndex player)
        {
            return isNewKeyPress(Keys.Space, controllingPlayer, out player) ||
                   isNewKeyPress(Keys.Enter, controllingPlayer, out player) ||
                   IsNewButtonPress(Buttons.A, controllingPlayer, out player) ||
                   IsNewButtonPress(Buttons.Start, controllingPlayer, out player);
        }

        public bool IsMenuCancel(PlayerIndex? controllingPlayer, out PlayerIndex player)
        {
            return isNewKeyPress(Keys.Escape, controllingPlayer, out player) ||
                   IsNewButtonPress(Buttons.B, controllingPlayer, out player) ||
                   IsNewButtonPress(Buttons.Back, controllingPlayer, out player);
        }

        public bool IsMenuUp(PlayerIndex? controllingPlayer)
        {
            PlayerIndex player;

            return isNewKeyPress(Keys.Up, controllingPlayer, out player) ||
                   IsNewButtonPress(Buttons.DPadUp, controllingPlayer, out player) ||
                   IsNewButtonPress(Buttons.LeftThumbstickUp, controllingPlayer, out player);
        }

        public bool IsMenuDown(PlayerIndex? controllingPlayer)
        {
            PlayerIndex player;

            return isNewKeyPress(Keys.Down, controllingPlayer, out player) ||
                   IsNewButtonPress(Buttons.DPadDown, controllingPlayer, out player) ||
                   IsNewButtonPress(Buttons.LeftThumbstickDown, controllingPlayer, out player);
        }

        public bool IsPauseGame(PlayerIndex? controllingPlayer)
        {
            PlayerIndex player;

            return isNewKeyPress(Keys.Escape, controllingPlayer, out player) ||
                   IsNewButtonPress(Buttons.Back, controllingPlayer, out player) ||
                   IsNewButtonPress(Buttons.Start, controllingPlayer, out player);
        }
        #endregion
    }
}

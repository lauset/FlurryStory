using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlurrysStory
{
    public class InputManager
    {
        KeyboardState currentkeyState, preKeyState;

        private static InputManager instance;

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputManager();

                return instance;
            }
        }

        public void Update()
        {
            Console.WriteLine(currentkeyState);
            preKeyState = currentkeyState;
            if (!ScreenManager.Instance.IsTransitioning)
                currentkeyState = Keyboard.GetState();
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentkeyState.IsKeyDown(key) && preKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentkeyState.IsKeyUp(key) && preKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentkeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }
    }
}

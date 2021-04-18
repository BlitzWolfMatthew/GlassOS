using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using KB = Cosmos.System.KeyboardManager;
using KEY = Cosmos.System.KeyEvent;

namespace GlassOS.Lib.IO
{
    public static class KeyboardPS2
    {
        public static bool CapsLockPressed = false;
        public static bool ShiftPressed = false;
        public static bool EnterPressed = false;
        public static bool EscapePressed = false;
        public static bool ControlPressed = false;
        public static bool AltPressed = false;
        public static KEY CurrentKey;
        public static KEY PreviousKey;

        public static void Update()
        {
            // get current key down
            if (KB.KeyAvailable)
            {
                if (KB.TryReadKey(out CurrentKey))
                {
                    PreviousKey = CurrentKey;
                }
            }
            else { CurrentKey = null; }

            // check enter
            EnterPressed = (CurrentKey.Key == Sys.ConsoleKeyEx.Enter);

            // check esapce
            EscapePressed = (CurrentKey.Key == Sys.ConsoleKeyEx.Escape);

            // check control keys
            CapsLockPressed = KB.CapsLock;
            ShiftPressed = KB.ShiftPressed;
            ControlPressed = KB.ControlPressed;
            AltPressed = KB.AltPressed;
        }

        public static bool IsKeyDown(Sys.ConsoleKeyEx key)
        {
            if (CurrentKey.Key == key) { return true; }
            else { return false; }
        }

        public static bool IsKeyDown(char key)
        {
            if (CurrentKey.KeyChar == key) { return true; }
            else { return false; }
        }

        public static bool IsKeyUp(Sys.ConsoleKeyEx key)
        {
            if (CurrentKey.Key != key) { return true; }
            else { return false; }
        }
    }
}

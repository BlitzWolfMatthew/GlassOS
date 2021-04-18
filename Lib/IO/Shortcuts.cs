using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

using GlassOS.Lib.IO;
using Cosmos.System;

namespace GlassOS.Lib.IO
{
    static class Shortcuts
    {
        public static void GetShortcuts()
        {
            KeyEvent input;
            KeyboardManager.TryReadKey(out input);

            if (input.Key == ConsoleKeyEx.LWin)
            {
                GlassOS.Lib.Graphics.Terminal.ClearAppBuffer(ConsoleColor.Black);
                GlassOS.External.OS_Interface.Desktop.DrawDesktop();
            }

            if (input.Key == ConsoleKeyEx.F9)
            {
                GlassOS.Lib.Graphics.Terminal.ClearAppBuffer(ConsoleColor.Black);
                GlassOS.External.OS_Applications.Settings.Start();
            }

            if (input.Key == ConsoleKeyEx.F12)
            {
                throw new GlassOS.Lib.OSException.IntendedCrashException("F12 key pressed");
            }

            if (Sys.KeyboardManager.ControlPressed && Sys.KeyboardManager.ShiftPressed && input.Key == ConsoleKeyEx.S)
            {
                GlassOS.Lib.Graphics.Terminal.ClearAppBuffer(ConsoleColor.Black);
                GlassOS.External.OS_Applications.Settings.Start();
            }
        }
    }
}

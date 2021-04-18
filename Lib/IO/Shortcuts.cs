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

            if (input.Key == ConsoleKeyEx.F12)
            {
                throw new GlassOS.Lib.OSException.IntendedCrashException("F12 key pressed");
            }

            if (Sys.KeyboardManager.ControlPressed && Sys.KeyboardManager.ShiftPressed)
            {
                switch (input.Key)
                {
                    case ConsoleKeyEx.S:
                        GlassOS.Lib.Graphics.Terminal.ClearAppBuffer(ConsoleColor.Black);
                        GlassOS.External.OS_Applications.Settings.Start();
                        break;
                    case ConsoleKeyEx.Backquote:
                        GlassOS.Lib.Graphics.Terminal.ClearAppBuffer(ConsoleColor.Black);
                        GlassOS.Lib.Graphics.Terminal.Update();
                        
                        Internal.Implementation.Windmill.Applications.apps.Add(new Internal.Implementation.Windmill.Windmill(4096, new byte[]
                        {
                            //add hello world to mem at loc 0x0f
                            0x12, 0x00, 0x00, 0x00, 0x0F, 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x77, 0x6F, 0x72, 0x6C, 0x64, 0x21, 0x00,
                            //print string starting at loc 0x0f
                            0x21, 0x00, 0x00, 0x00, 0x0F, 
                            //end program
                            0x00,
                        }));
                        
                        break;
                }
            }
        }
    }
}

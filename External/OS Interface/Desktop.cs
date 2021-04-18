using System;
using System.Collections.Generic;
using System.Text;

using GlassOS.Lib.Graphics;

namespace GlassOS.External.OS_Interface
{
    static class Desktop             
    {
        public static void DrawDesktop()
        {
            Terminal.SetCursorPos(0, 0);
            var fileEntryArr = GlassOS.Kernel.fs.GetDirectoryListing(@"0:\");
            Terminal.ForegroundColor = ConsoleColor.Yellow;
            Terminal.WriteLine("<---------> (Desktop) <--------->");
            for (int i = 0; i < fileEntryArr.Count; i++)
            {
                Terminal.ForegroundColor = ConsoleColor.Yellow; Terminal.WriteLine("|                               |");
                Terminal.ForegroundColor = ConsoleColor.White; Terminal.SetCursorPos(1, i+1); Terminal.WriteLine(fileEntryArr[i].mName);
            }
            Terminal.ForegroundColor = ConsoleColor.Yellow;
            Terminal.WriteLine("<------------------------------->");
        }
    }
}

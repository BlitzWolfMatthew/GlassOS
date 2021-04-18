using System;
using System.Collections.Generic;
using System.Text;

using GlassOS.Lib.Graphics;

namespace GlassOS.Internal.Startup
{
    static class Startup
    {
        public static void StartOS()
        {
            string space = "             "; //13 spaces to center the text
            foreach (string line in GlassOS.External.Artwork.ConsoleArt.logo)
                GlassOS.Lib.Graphics.Terminal.WriteLine(space + line);

            Terminal.SetCursorPos(65, 9);
            Terminal.ForegroundColor = ConsoleColor.Blue; Terminal.Write("Ro");
            Terminal.ForegroundColor = ConsoleColor.Yellow; Terminal.Write("man");
            Terminal.ForegroundColor = ConsoleColor.Red; Terminal.Write("ia");

            Terminal.ForegroundColor = ConsoleColor.White;
            Terminal.SetCursorPos(0, 10);

            Terminal.ReadLine();
            Terminal.Clear();
        }
    }
}

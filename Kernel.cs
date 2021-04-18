using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using GlassOS.Lib.Graphics;

namespace GlassOS
{
    public class Kernel : Sys.Kernel
    {
        public static Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();

        protected override void BeforeRun()
        { 
            Lib.Graphics.VGADriverII.Initialize(VGAMode.Text90x60);
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Sys.MouseManager.MouseSensitivity = 0.5f;
            Sys.MouseManager.ScreenWidth = 90;
            Sys.MouseManager.ScreenHeight = 60;
            Terminal.Clear();

            GlassOS.Internal.Startup.Startup.StartOS();
        }

        byte mouseBg; uint[] mouseLoc = { 0, 0};
        protected unsafe override void Run()
        {
            try
            {
                GlassOS.External.OS_Interface.UIBars.DrawTaskBar();
                GlassOS.Lib.IO.Shortcuts.GetShortcuts();

                if (Sys.MouseManager.X != mouseLoc[0] || Sys.MouseManager.Y != mouseLoc[1])
                {
                    VGADriverII.Buffer[(mouseLoc[0] + (mouseLoc[1] * 90)) * 2 + 1] = mouseBg;
                    mouseLoc[0] = Sys.MouseManager.X; mouseLoc[1] = Sys.MouseManager.Y;
                    mouseBg = (VGADriverII.Buffer[(Sys.MouseManager.X + (Sys.MouseManager.Y * 90)) * 2 + 1]);
                    VGADriverII.Buffer[(Sys.MouseManager.X + (Sys.MouseManager.Y * 90)) * 2 + 1] = ((byte)ConsoleColor.DarkBlue) << 4;
                }                
            }
            catch (Exception e)
            {
                ThrowBlueScreen(e);
            }            
        }

        private void ThrowBlueScreen(Exception e)
        {
            Terminal.ForegroundColor = ConsoleColor.White;
            Terminal.BackgroundColor = ConsoleColor.DarkBlue;
            Terminal.Clear();

            Terminal.WriteLine("Your PC has run into a problem and has been shut down to prevent damage to the system.");
            Terminal.WriteLine();
            Terminal.WriteLine("Error message:");
            Terminal.WriteLine(e.ToString());
            Terminal.WriteLine("\nPlease report this issue to the developers!\nhttps://github.com/BlitzWolfMatthew/GlassOS");
            int y = Terminal.CursorY;
            Terminal.SetCursorPos(0, 59); Terminal.Write("@BlitzWolfMatthew Corporation - 2021");

            Terminal.SetCursorPos(0, y);
            Terminal.WriteLine("\nPress enter to reboot, press delete to shut down: ");
            while (true)
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    Sys.Power.Reboot();
                else if (Console.ReadKey(true).Key == ConsoleKey.Delete)
                    Sys.Power.Shutdown();
        }
    }
}

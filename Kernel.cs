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
            Sys.MouseManager.MouseSensitivity = 0.4f;
            Sys.MouseManager.ScreenWidth = 90;
            Sys.MouseManager.ScreenHeight = 60;
            Sys.MouseManager.X = 45; Sys.MouseManager.Y = 30;
            Terminal.Clear();
            Terminal.Update();

            GlassOS.Internal.Startup.Startup.StartOS();
        }

        static byte mouseBg; public static uint[] mouseLoc = { 0, 0};
        protected unsafe override void Run()
        {
            try
            {
                GlassOS.External.OS_Interface.UIBars.DrawTaskBar();
                Terminal.Update();

                GlassOS.Lib.IO.Shortcuts.GetShortcuts();

                foreach (Internal.Implementation.Windmill.Windmill app in Internal.Implementation.Windmill.Applications.apps)
                {
                    if (app.RunNext())
                        Internal.Implementation.Windmill.Applications.apps.Remove(app);
                    Terminal.Update();
                }      

                if (Sys.MouseManager.X != mouseLoc[0] || Sys.MouseManager.Y != mouseLoc[1])
                {
                    Terminal.BackBuffer[(mouseLoc[0] + (mouseLoc[1] * 90)) * 2 + 1] = mouseBg;
                    mouseLoc[0] = Sys.MouseManager.X; mouseLoc[1] = Sys.MouseManager.Y;
                    mouseBg = (Terminal.BackBuffer[(Sys.MouseManager.X + (Sys.MouseManager.Y * 90)) * 2 + 1]);
                    Terminal.BackBuffer[(Sys.MouseManager.X + (Sys.MouseManager.Y * 90)) * 2 + 1] = ((byte)ConsoleColor.DarkBlue) << 4;
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
            Terminal.Clear(ConsoleColor.DarkBlue);

            Terminal.WriteLine("Your PC has run into a problem and has been shut down to prevent damage to the system.");
            Terminal.WriteLine();
            Terminal.WriteLine("Error:");
            Terminal.WriteLine(e.ToString());
            Terminal.WriteLine("\nPlease report this issue to the developers!\nhttps://github.com/BlitzWolfMatthew/GlassOS");
            int y = Terminal.CursorY;
            Terminal.SetCursorPos(0, 59); Terminal.Write("@BlitzWolfMatthew Corporation - 2021");

            Terminal.SetCursorPos(0, y);
            Terminal.WriteLine("\nPress enter to reboot, press delete to shut down: ");
            Terminal.Update();

            while (true)
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    Sys.Power.Reboot();
                else if (Console.ReadKey(true).Key == ConsoleKey.Delete)
                    Sys.Power.Shutdown();
        }
    }
}

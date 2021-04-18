using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

using Cosmos.System.Graphics;
using GlassOS.Lib.Graphics;

using Cosmos.HAL;

namespace GlassOS.External.OS_Interface
{
    static class UIBars
    {
        static string hour, min, sec, time, day, mon, date;
        public static void DrawTaskBar()
        {
            for (int y = 58; y < 60; y++)
                for (int x = 2; x < 90; x++)
                    Terminal.PutCharacter(x, y, ' ', ConsoleColor.Black, ConsoleColor.Gray);

            string xVal = Kernel.mouseLoc[0].ToString(); if (xVal.Length == 1) xVal = "0" + xVal;
            string yVal = Kernel.mouseLoc[1].ToString(); if (yVal.Length == 1) yVal = "0" + yVal;
            Terminal.PutCharacter(0, 58, xVal[0], ConsoleColor.Black, ConsoleColor.Gray);
            Terminal.PutCharacter(1, 58, xVal[1], ConsoleColor.Black, ConsoleColor.Gray);
            Terminal.PutCharacter(0, 59, yVal[0], ConsoleColor.Black, ConsoleColor.Gray);
            Terminal.PutCharacter(1, 59, yVal[1], ConsoleColor.Black, ConsoleColor.Gray);

            //time
            hour = RTC.Hour.ToString(); if (hour.Length == 1) hour = "0" + hour;
            min = RTC.Minute.ToString(); if (min.Length == 1) min = "0" + min;
            sec = RTC.Second.ToString(); if (sec.Length == 1) sec = "0" + sec;
            time = hour + ":" + min + ":" + sec + "  "; 
            //date
            day = RTC.DayOfTheMonth.ToString(); if (day.Length == 1) day = "0" + day;
            mon = RTC.Month.ToString(); if (mon.Length == 1) mon = "0" + mon;
            date = day + "/" + mon + "/" + RTC.Year.ToString() + "  ";

            for (int i = 0; i < 8; i++)
                Terminal.PutCharacter(80+i, 58, time[i], ConsoleColor.Black, ConsoleColor.Gray);

            for (int i = 0; i < 8; i++)
                Terminal.PutCharacter(80+i, 59, date[i], ConsoleColor.Black, ConsoleColor.Gray);
        }

        public static void DrawStartMenu()
        {
            
        }
    }
}

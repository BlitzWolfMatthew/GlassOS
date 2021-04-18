using System;
using System.Collections.Generic;
using System.Text;

using GlassOS.Lib.App;

namespace GlassOS.Internal.Implementation.Windmill.Lib
{
    static class Output
    {
        public static void FindFunction(Windmill super)
        {
            super.index++;
            switch (super.program[super.index - 1] % 16)
            {
                case 0x00:
                    PrintChar(super);
                    break;
                case 0x01:
                    PrintString(super);
                    break;
                case 0x02:
                    SetForeGroundColor(super);
                    break;
                case 0x03:
                    SetBackGroundColor(super);
                    break;
            }
        }

        static void SetBackGroundColor(Windmill super)
        {
            byte type = super.program[super.index];
            super.terminal.BackgroundColor = (ConsoleColor)type;
        }

        static void SetForeGroundColor(Windmill super)
        {
            byte type = super.program[super.index];
            super.terminal.ForegroundColor = (ConsoleColor)type;
        }

        static void PrintChar(Windmill super)
        {
            int loc = Memory.GetRamLoc(super);
            super.terminal.Write(((char) super.ram[loc]).ToString());
        }

        static void PrintString(Windmill super)
        {
            int loc = Memory.GetRamLoc(super);
            string capture = "";

            for (; super.ram[loc] != 0; loc++)
            {
                capture += (char) super.ram[loc];
            }
            super.terminal.Write(capture);
        }
    }
}

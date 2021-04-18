using System;
using System.Collections.Generic;
using System.Text;

namespace GlassOS.Internal.Implementation.Windmill
{
    class Windmill
    {
        public byte[] program;

        public byte[] ram;
        public int index;

        public GlassOS.Lib.App.AppTerminal terminal = new GlassOS.Lib.App.AppTerminal();

        public Windmill(uint mAlloc, byte[] program)
        {
            this.program = program;
            ram = new byte[mAlloc];
        }

        //returns true if the program ended
        public bool RunNext()
        {
            if (program[index] == 0)
                return true;
            else
                return false;
            FindCommand();
            index++;
        }

        public void FindCommand()
        {
            switch (program[index] / 16)
            {
                case 0x01:
                    Lib.Memory.FindFunction(this);
                    break;
                case 0x02:
                    Lib.Output.FindFunction(this);
                    break;
                case 0x03:
                    Lib.Input.FindFunction(this);
                    break;
                case 0x04:
                    Lib.Math.FindFunction(this);
                    break;
                case 0x05:
                    Lib.Utilities.FindFunction(this);
                    break;
            }
        }
    }
}


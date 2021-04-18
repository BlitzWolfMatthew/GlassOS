using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

using GlassOS.Lib.Graphics;

namespace GlassOS.External.OS_Applications
{
    static class Settings
    {
        public static void Start()
        {
            Terminal.Clear();

            Terminal.SetCursorPos(0, 0);
            Terminal.WriteLine("---(System specifications)---");
            Terminal.WriteLine("Uptime: " + Cosmos.Core.CPU.GetCPUUptime());
            Terminal.WriteLine("CPU : " + Cosmos.Core.CPU.GetCPUVendorName() + "  @ " + Cosmos.Core.CPU.GetCPUCycleSpeed());
            uint uRam = Cosmos.Core.CPU.GetAmountOfRAM() - (Cosmos.Core.CPU.GetEndOfKernel() + 1024) / 1048576; 
            Terminal.WriteLine("RAM : " + Cosmos.Core.CPU.GetAmountOfRAM() + "MB; Of which " + uRam + "MB usable.");
            Terminal.WriteLine("Disk - " + Kernel.fs.GetFileSystemType("0:/") + ": " + 
                (Kernel.fs.GetTotalSize(@"0:\") - Kernel.fs.GetAvailableFreeSpace(@"0:\")) / 1048576 + "/" 
                + Kernel.fs.GetTotalSize(@"0:\") / 1048576 + " MB");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GlassOS.Lib.OSException
{
    class IntendedCrashException : Exception
    {
        public IntendedCrashException(string additionalData) : base("IntendedCrashException: " + additionalData) {}
        public IntendedCrashException() : base("IntendedCrashException") { }
    }
}

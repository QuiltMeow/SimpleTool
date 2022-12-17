using System;
using System.Runtime.InteropServices;

namespace DisableScreenSaver
{
    public static class Util
    {
        [Flags]
        public enum ExecutionState : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern ExecutionState SetThreadExecutionState(ExecutionState esFlag);

        public static void preventScreenSaver(bool enable)
        {
            SetThreadExecutionState(enable ? ExecutionState.ES_DISPLAY_REQUIRED | ExecutionState.ES_CONTINUOUS : ExecutionState.ES_CONTINUOUS);
        }
    }
}
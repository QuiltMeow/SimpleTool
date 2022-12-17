using System;
using System.Runtime.InteropServices;

namespace PreventADLogin
{
    public static class Util
    {
        [DllImport("Wtsapi32.dll")]
        private static extern bool WTSQuerySessionInformation(IntPtr hServer, int sessionId, WTSInfoClass wtsInfoClass, out IntPtr ppBuffer, out int pByteReturn);

        [DllImport("Wtsapi32.dll")]
        private static extern void WTSFreeMemory(IntPtr pMemory);

        private enum WTSInfoClass
        {
            WTSInitialProgram,
            WTSApplicationName,
            WTSWorkingDirectory,
            WTSOEMId,
            WTSSessionId,
            WTSUserName,
            WTSWinStationName,
            WTSDomainName,
            WTSConnectState,
            WTSClientBuildNumber,
            WTSClientName,
            WTSClientDirectory,
            WTSClientProductId,
            WTSClientHardwareId,
            WTSClientAddress,
            WTSClientDisplay,
            WTSClientProtocolType,
            WTSIdleTime,
            WTSLogonTime,
            WTSIncomingByte,
            WTSOutgoingByte,
            WTSIncomingFrame,
            WTSOutgoingFrame,
            WTSClientInfo,
            WTSSessionInfo,
            WTSSessionInfoEx,
            WTSConfigInfo,
            WTSValidationInfo,
            WTSSessionAddressV4,
            WTSIsRemoteSession
        }

        public static string getUserName(int sessionId, bool prependDomain = true)
        {
            IntPtr buffer;
            int length;
            string ret = "SYSTEM";

            if (WTSQuerySessionInformation(IntPtr.Zero, sessionId, WTSInfoClass.WTSUserName, out buffer, out length) && length > 1)
            {
                ret = Marshal.PtrToStringAnsi(buffer);
                WTSFreeMemory(buffer);

                if (prependDomain)
                {
                    if (WTSQuerySessionInformation(IntPtr.Zero, sessionId, WTSInfoClass.WTSDomainName, out buffer, out length) && length > 1)
                    {
                        ret = $"{Marshal.PtrToStringAnsi(buffer)}\\{ret}";
                        WTSFreeMemory(buffer);
                    }
                }
            }
            return ret;
        }
    }
}
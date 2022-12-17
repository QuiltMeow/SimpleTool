using System;

namespace PreventADLogin
{
    public static class Whitelist
    {
        private static readonly string[] ALLOW_USER = {
            "SYSTEM",
            "YUNHEI\\AN3512",
            "Administrator",
            "User",
            "AkatsukiNeko",
            "AkatsukiNya",
            "Neko",
            "Quilt",
            "SmallQuilt"
        };

        public static bool isAllowUser(string userName)
        {
            foreach (string name in ALLOW_USER)
            {
                if (userName == $"{name}")
                {
                    return true;
                }

                if (!name.Contains("\\"))
                {
                    if (userName == $"{Environment.MachineName}\\{name}")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
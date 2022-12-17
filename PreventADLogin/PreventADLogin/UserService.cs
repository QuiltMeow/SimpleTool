using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace PreventADLogin
{
    public partial class UserService : ServiceBase
    {
        private const string NAME = "AD 登入過濾服務";

        public UserService()
        {
            InitializeComponent();
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            SessionChangeReason reason = changeDescription.Reason;
            int sessionId = changeDescription.SessionId;
            EventLog.WriteEntry(NAME, $"[{DateTime.Now.ToLongTimeString()}] Session 變更 : {reason} ID : {sessionId}");

            bool shouldCheck = false;
            string userName = Util.getUserName(sessionId);
            switch (reason)
            {
                case SessionChangeReason.SessionLogon:
                    {
                        shouldCheck = true;
                        EventLog.WriteEntry(NAME, $"使用者登入 : {userName}");
                        break;
                    }
                case SessionChangeReason.SessionUnlock:
                    {
                        shouldCheck = true;
                        EventLog.WriteEntry(NAME, $"使用者解除鎖定 : {userName}");
                        break;
                    }
            }

            if (shouldCheck)
            {
                if (!Whitelist.isAllowUser(userName))
                {
                    BSOD.triggerBSOD();
                }
            }
        }
    }
}
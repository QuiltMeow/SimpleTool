using System.ServiceProcess;

namespace PreventADLogin
{
    public static class Program
    {
        public static void Main()
        {
            ServiceBase[] serviceToRun = new ServiceBase[]
            {
                new UserService()
            };
            ServiceBase.Run(serviceToRun);
        }
    }
}
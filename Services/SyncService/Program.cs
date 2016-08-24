using System.ServiceProcess;

namespace Services.SyncService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new SyncTemperature()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WebServer;

namespace ShockDatacaster
{
    public partial class Service : ServiceBase
    {
        private Server webServer;

        /// <summary>
        /// The system service for ShockDatacaster
        /// </summary>
        public Service()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The main entry point for the application
        /// </summary>
        static void Main(string[] args)
        {
            // Create instance of the service
            Service service = new Service();
            
            // Launch service
            if (!Environment.UserInteractive) // If running as a system service
            {
                // Run the service
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { service };
                ServiceBase.Run(ServicesToRun);
            }
            else // Else if running as a console application
            {
                //
                // Change headers output colour
                //
                // Write header
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("ShockDatacaster");
                Console.WriteLine("---------------");
                Console.WriteLine("Press any key to exit");
                Console.WriteLine();
                Console.ResetColor();
                // Start the service
                service.OnStart(args);
                // Wait for key entry to exit
                Console.ReadKey();
                // Stop the service
                service.OnStop();
            }
        }

        /// <summary>
        /// Carry out service launch
        /// </summary>
        /// <param name="args">Applicaton arguments</param>
        protected override void OnStart(string[] args)
        {
            // Start web server on port 7100
            try
            {
                webServer = new Server(7100);
                webServer.ServerName = "ShockDatacaster";
            }
            catch (InvalidOperationException e)
            {
                //
                // Log exception
                //
            }
        }

        /// <summary>
        /// Stop the service
        /// </summary>
        protected override void OnStop()
        {
            if (webServer != null)
            {
                webServer.Dispose();
            }
        }
    }
}

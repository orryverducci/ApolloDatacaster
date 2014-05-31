using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ShockDatacaster
{
    public partial class Service : ServiceBase
    {
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
                // Write header
                Console.WriteLine("ShockDatacaster");
                Console.WriteLine("---------------");
                Console.WriteLine("Press any key to exit");
                Console.WriteLine();
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
        }

        /// <summary>
        /// Stop the service
        /// </summary>
        protected override void OnStop()
        {
        }
    }
}

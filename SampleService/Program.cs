﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SampleService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.  Do not change this code.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Feeder()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}

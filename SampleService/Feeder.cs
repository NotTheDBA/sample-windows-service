using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SampleService
{
    /// <summary>
    /// The Feeder class is the Windows service core of the project code.
    /// </summary>
    public partial class Feeder : ServiceBase
    {
        public Feeder()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}

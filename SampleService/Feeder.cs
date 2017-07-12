using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using APISample;

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

            // This sets up a timer to auto-trigger however often you need it.  Be aware that if it happens often, you may have multiple threads 
            // running at the same time, so all code should be able to run in parallel.
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 10000; // 10000 = 10.000 seconds
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        protected override void OnStart(string[] args)
        {
            //If you need to initialize any variables when timer starts, put it here.          
            System.Console.WriteLine("Sample Event Feeder service is starting.");
        }

        protected override void OnStop()
        {
            //If you need to clean up any objects when the thread ends, put it here.          
            System.Console.WriteLine("Sample Event Feeder service is stopping.");
        }


        /// <summary>
        /// This is the function that is triggered when the timer Interval elapses 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            //Put the code you want to fire every interval here.  This code needs to be able to run in multiple parallel threads. 
            APISample.SampleWrapper.DoSomething("Sample Event Feeder service was called.", "It has processed a timed event.");
        }
    }
}

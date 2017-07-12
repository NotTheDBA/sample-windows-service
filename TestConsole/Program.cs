using System;
//using System.Timers;

namespace TestConsole
{
    /// <summary>
    /// The TestFeeder project is a Console (command line) version of the project code.   It allows you to step through the code in development to view and test
    /// the application's behavior.   The custom methods you create will be identical to the ones which run in the Feeder service class.
    /// </summary>
    static class TestFeeder
    {
        static void Main(string[] args)
        {
            OnStart(args);
            OnTimer();
            OnStop();
        }

        static void OnStart(string[] args)
        {
            //If you need to initialize any variables when timer starts, put it here.          
            System.Console.WriteLine("Sample Event Feeder service is starting.");
        }

        static void OnStop()
        {
            //If you need to clean up any objects when the thread ends, put it here.          
            System.Console.WriteLine("Sample Event Feeder service is stopping.");
        }

        /// <summary>
        /// This is the function that is triggered when the timer Interval elapses 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        //public static void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        public static void OnTimer()  //Note that we are not supporting the default Timer paramemeters.  That is beyond the scope.
        {
            //Put the code you want to fire every interval here.  This code needs to be able to run in multiple parallel threads.          
            System.Console.WriteLine("Sample Event Feeder service is has processed a timed event.");
        }

    }
}
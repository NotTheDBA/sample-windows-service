using System;
using System.Data;
using System.Diagnostics;
//using System.Timers;
using APISample;
using Common;
using SampleService;
using System.Globalization;

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

            //TODO:  Register the Assembly name; this value will need to be set up in the registry by the service installer for it to work.
            Common.Logging.WriteEvent(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, "Sample Event Feeder service is starting.", EventLogEntryType.Information);
        }

        static void OnStop()
        {
            //If you need to clean up any objects when the thread ends, put it here.          
            //System.Console.WriteLine("Sample Event Feeder service is stopping.");
            Common.Logging.WriteEvent(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, "Sample Event Feeder service is stopping.", EventLogEntryType.Information);
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
            APISample.SampleWrapper.DoSomething("Sample Event Feeder service was called.", "It has processed a timed event.");

            //The TestFeeder class only pulls test events.
            SampleService.DataFactory df = new DataFactory();
            DataSet events = df.GetTestEvents();

            //After this, the code should be identical to the OnTimer() method.
            int id;
            DateTime eventDate;
            string eventType;

            //I used constants so I can quickly add standard values, which reduces the chances of a mis-typed value for matching.
            const string event_CustomerRefresh = "Customer Refresh";

            foreach (DataRow dr in events.Tables[0].Rows)
            {

                // EventID, Email, EventType, [EventData], EventStatus, EventDate  s
                id = Convert.ToInt32(dr["EventID"].ToString().Trim());
                eventDate = Convert.ToDateTime(dr["EventDate"].ToString().Trim());
                eventType = dr["EventType"].ToString().Trim();
                try
                {
                    if (eventType == event_CustomerRefresh)
                    {
                        Identify(Convert.ToInt32(dr["EventData"].ToString().Trim()));
                        FinalizeEvent(id);
                    }
                    else
                    {
                        //If we don't find our event type, we assume it's not implemented yet and place it on hold.
                        df.UpdateEvent(id, "H");
                    }

                }
                catch (Exception ex)
                {
                    Common.Logging.WriteEvent(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, ex.Data.ToString(), EventLogEntryType.Error);
                    df.UpdateEvent(id, "E");
                }
            }

        }


        /// <summary>
        /// This is an example of updating the event record to show it has been successfully processed.
        /// </summary>
        /// <param name="EventID"></param>
        private static void FinalizeEvent(int EventID)
        {
            DataFactory df = new DataFactory();
            df.UpdateEvent(EventID, "C");

        }

        /// <summary>
        /// This is an example of a custom event.  It uses the DataFactory class to retrieve data from a stored procedure, then parses it to send to an API.
        /// </summary>
        /// <param name="CustID">Customer ID</param>
        private static void Identify(int CustID)
        {
            DataFactory df = new DataFactory();
            DataSet customer = df.GetCustomerData(CustID);

            foreach (DataRow dr in customer.Tables[0].Rows)
            {
                try
                {
                  APISample.SampleWrapper.DoSomething(dr["userid"].ToString().ToLower(CultureInfo.InvariantCulture).Trim(), dr["custid"].ToString().Trim());
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

    }
}
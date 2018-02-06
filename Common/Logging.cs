using System;
using System.Diagnostics;

namespace Common
{
    public static class Logging
    {
        public static void WriteEvent(String Application, String Entry, EventLogEntryType EventType)
        {
            //TODO:  Grant executing user permissions to read this registry key:  HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\EventLog
            EventLog objEventLog = new EventLog(System.Diagnostics.Process.GetCurrentProcess().ProcessName)
            {
                Source = Application
            };
            objEventLog.WriteEntry(Entry, EventType);
        }
    }
}

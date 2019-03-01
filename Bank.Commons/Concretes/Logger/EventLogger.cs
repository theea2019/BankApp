using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Commons.Abstractions;

namespace Bank.Commons.Concretes.Logger
{
    internal class EventLogger : LogBase
    {

        public override void Log(string message)
        {
            lock (lockObj)
            {
                EventLog m_EventLog = new EventLog();
                m_EventLog.Source = "XBankEventLog";
                m_EventLog.WriteEntry(message);
            }
        }
    }
}

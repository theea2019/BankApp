using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Commons.Abstractions;

namespace Bank.Commons.Concretes.Logger
{
    internal class DBLogger : LogBase
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        public override void Log(string message, bool isError)
        {
            lock (lockObj)
            {
                //Code to log data to the database
            }
        }
    }
}

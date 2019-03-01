using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Commons.Abstractions;
using Bank.Commons.Concretes.Logger;

namespace Bank.Commons.Concretes.Helpers
{
    public static class LogHelper
    {
        private static LogBase logger = null;
        public static void Log(LogTarget target, string message)
        {
            switch (target)
            {
                case LogTarget.File:
                    logger = new FileLogger();
                    logger.Log(message);
                    break;
                case LogTarget.Database:
                    logger = new DBLogger();
                    logger.Log(message);
                    break;
                case LogTarget.EventLog:
                    logger = new EventLogger();
                    logger.Log(message);
                    break;
                default:
                    return;
            }
        }
    }
}

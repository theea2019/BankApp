using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Commons.Abstractions;

namespace Bank.Commons.Concretes.Logger
{
    internal class FileLogger : LogBase
    {
        public string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["LoggingPath"]);
        public bool enableLogging = bool.Parse(ConfigurationManager.AppSettings["EnableLogging"].ToLower());
        
        public override void Log(string message)
        {
            if (enableLogging)
            {
                lock (lockObj)
                {
                    using (StreamWriter streamWriter = new StreamWriter(filePath))
                    {
                        streamWriter.WriteLine(message);
                        streamWriter.Close();
                    }
                }
            }
        }
    }
}

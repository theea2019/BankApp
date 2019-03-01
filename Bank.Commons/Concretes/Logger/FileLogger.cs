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
        private string _filePath;
        private bool _enableLogging;
        
        public override void Log(string message)
        {
            if (_enableLogging)
            {
                lock (lockObj)
                {
                    // TODO - Optimise and iterate the writing solution
                    using (StreamWriter streamWriter = new StreamWriter(_filePath))
                    {
                        streamWriter.WriteLine(message);
                        streamWriter.Close();
                    }
                }
            }
        }

        public FileLogger()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["LoggingPath"]).ToString();
            _enableLogging = bool.Parse(ConfigurationManager.AppSettings["EnableLogging"].ToLower());
        }
    }
}

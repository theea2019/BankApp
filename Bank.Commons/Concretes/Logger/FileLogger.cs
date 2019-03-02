using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Bank.Commons.Abstractions;
using Bank.Commons.Concretes.Helpers;

namespace Bank.Commons.Concretes.Logger
{
    internal class FileLogger : LogBase
    {
        private string _filePath;

        public override void Log(string message, bool isError)
        {
            Guid guid = Guid.NewGuid();
            if (isError)
            {
                lock (lockObj)
                {
                    FileHelper.WriteFile(_filePath, guid.ToString() + "-" + DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.") + "Error.txt", message);
                }
            }
            else
            {
                lock (lockObj)
                {
                    FileHelper.WriteFile(_filePath, guid.ToString() + "-" + DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.") + "Log.txt", message);
                }
            }

        }

        public FileLogger()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["LoggingPath"]).ToString();
        }
    }
}
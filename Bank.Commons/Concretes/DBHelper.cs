using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Commons.Concretes
{
    public class DBHelper : IDisposable
    {
        private static object _locker = new object();
        private static DBHelper _dbHelper;
        private static DbConnection _dbConnection;
        private static DbProviderFactory _dbProviderFactory;
        
        private DBHelper(string providerName)
        {
            _dbProviderFactory = DbProviderFactories.GetFactory(providerName);
        }

        public static DbConnection GetConnection(string providerName, string connectionString)
        {
            lock (_locker)
            {
                if (_dbConnection == null)
                {
                    _dbHelper = new DBHelper(providerName);
                    _dbConnection = _dbProviderFactory.CreateConnection();
                    _dbConnection.ConnectionString = connectionString;
                }
            }

            return _dbConnection;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}

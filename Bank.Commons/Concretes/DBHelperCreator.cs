using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Commons.Concretes
{
    public static class DBHelperCreator
    {
        private const string MSSQLFormater = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
        private const string OracleFormater = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={4})))(CONNECT_DATA=(sid={1})));User Id={2};Password={3}";
        private const string MySQLFormater = "server={0};database={1};persistsecurityinfo=True;user id={2};password={3}";
        private const string SQLiteFormater = "Data Source={0};Default Database Type=String";

        private static DbProviderFactory dbProvider = null;

        private static readonly string[] Providers = {
            "System.Data.SqlClient",
            "Oracle.ManagedDataAccess.Client",
            "MySql.Data.MySqlClient",
            "System.Data.SQLite"
        };

        public static DatabaseType GetDatabaseType(string provider)
        {
            var result = default(DatabaseType);

            if (string.Compare(Providers[0], provider, StringComparison.OrdinalIgnoreCase) == 0)
            {
                result = DatabaseType.MSSQL;
            }
            else if (string.Compare(Providers[1], provider, StringComparison.OrdinalIgnoreCase) == 0)
            {
                result = DatabaseType.Oracle;
            }
            else if (string.Compare(Providers[2], provider, StringComparison.OrdinalIgnoreCase) == 0)
            {
                result = DatabaseType.MySQL;
            }
            else
            {
                result = DatabaseType.SQLite;
            }

            return result;
        }

        public static string GetConnectionString(DatabaseType database, string host, string instance, string account, string password, int? port = null)
        {
            var connectionString = string.Empty;

            switch (database)
            {
                case DatabaseType.MSSQL:
                    connectionString = String.Format(MSSQLFormater, host, instance, account, password);
                    break;
                case DatabaseType.Oracle:
                    connectionString = String.Format(OracleFormater,host,instance, account, password, port.HasValue ? port : 1521);
                    break;
                case DatabaseType.MySQL:
                    connectionString = String.Format(MySQLFormater, host, instance, account, password);
                    break;
                case DatabaseType.SQLite:
                    connectionString = String.Format(SQLiteFormater, host);
                    break;
            }

            return connectionString;
        }
    }
}

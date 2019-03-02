using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Commons;
using Bank.DataAccess.Abstractions;
using Bank.Models.Concretes;

namespace Bank.DataAccess.Concretes
{
    public class TranscationsRepository : IRepository<Transactions>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public TranscationsRepository()
        {
            _connectionString = DBHelper.GetConnectionString();
            _dbProviderName = DBHelper.GetConnectionProvider();
            _dbProviderFactory = DbProviderFactories.GetFactory(_dbProviderName);
        }

        public bool DeletedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("DELETE ");
                query.Append("FROM [dbo].[tbl_Transactions] ");
                query.Append("WHERE ");
                query.Append("[tbl_Transactions] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Transactions] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();
                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception(
                                "Deleting Error for entity [tbl_Transactions] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                // TODO - ADD Logging Commons
                Console.WriteLine(ex.Message);
                throw new Exception("TransactionsRepository::Insert:Error occured.", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            // Check the Dispose method called before.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Clean the resources used.
                    _dbProviderFactory = null;
                }

                _bDisposed = true;
            }
        }

        public bool Insert(Transactions entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Transactions] ");
                query.Append("( [TransactionAmount], [TransactorAccountNumber], [RecieverAccountNumber], [TransactionDate], [isSuccess] ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @TransactionAmount, @TransactorAccountNumber, @RecieverAccountNumber, @TransactionDate, @isSuccess ) ");
                query.Append("SELECT @intErrorCode=@@ERROR;");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Transactions] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@TransactionAmount", CsType.Decimal, ParameterDirection.Input, entity.TransactionAmount);
                        DBHelper.AddParameter(dbCommand, "@TransactorAccountNumber", CsType.Int, ParameterDirection.Input, entity.TransactorAccountNumber);
                        DBHelper.AddParameter(dbCommand, "@RecieverAccountNumber", CsType.Int, ParameterDirection.Input, entity.RecieverAccountNumber);
                        DBHelper.AddParameter(dbCommand, "@TransactionDate", CsType.DateTime, ParameterDirection.Input, entity.TransactionDate);
                        DBHelper.AddParameter(dbCommand, "@isSuccess", CsType.Boolean, ParameterDirection.Input, entity.isSuccess);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Transactions] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                // TODO - ADD Logging Commons
                Console.WriteLine(ex.Message);
                throw new Exception("TransactionsRepository::Insert:Error occured.", ex);
            }
        }

        public IList<Transactions> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Transactions> transactions = new List<Transactions>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[TransactionID], [TransactionAmount], [TransactorAccountNumber], [RecieverAccountNumber], [TransactionDate], [isSuccess] ");
                query.Append("FROM [dbo].[tbl_Transactions] ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Transactions] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters - None

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int,
                            ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Transactions();
                                    entity.TransactionID = reader.GetInt32(0);
                                    entity.TransactionAmount = reader.GetDecimal(1);
                                    entity.TransactorAccountNumber = reader.GetInt32(2);
                                    entity.RecieverAccountNumber = reader.GetValue(3) == DBNull.Value ? (int?)null: reader.GetInt32(3);
                                    entity.TransactionDate = reader.GetDateTime(4);
                                    entity.isSuccess = reader.GetBoolean(5);
                                    transactions.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Transactions] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return transactions;
            }
            catch (Exception ex)
            {
                // TODO - ADD Logging Commons
                Console.WriteLine(ex.Message);
                throw new Exception("TransactionsRepository::SelectAll:Error occured.", ex);
            }
        }

        public Transactions SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Transactions transaction = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[TransactionID], [TransactionAmount], [TransactorAccountNumber], [RecieverAccountNumber], [TransactionDate], [isSuccess] ");
                query.Append("FROM [dbo].[tbl_Transactions] ");
                query.Append("WHERE ");
                query.Append("[TransactorAccountNumber] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Transactions] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Transactions();
                                    entity.TransactionID = reader.GetInt32(0);
                                    entity.TransactionAmount = reader.GetDecimal(1);
                                    entity.TransactorAccountNumber = reader.GetInt32(2);
                                    entity.RecieverAccountNumber = reader.GetInt32(3);
                                    entity.TransactionDate = reader.GetDateTime(4);
                                    entity.isSuccess = reader.GetBoolean(6);
                                    transaction = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Transactions] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return transaction;
            }
            catch (Exception ex)
            {
                // TODO - ADD Logging Commons
                Console.WriteLine(ex.Message);
                throw new Exception("TransactionsRepository::SelectById:Error occured.", ex);
            }
        }

        public bool Update(Transactions entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Transactions] ");
                query.Append("( [TransactionAmount] = @TransactionAmount, [TransactorAccountNumber] = @TransactorAccountNumber, [RecieverAccountNumber] =  @RecieverAccountNumber, [TransactionDate] = @TransactionDate, [isSuccess] = @isSuccess ) ");
                query.Append("WHERE ");
                query.Append("[TransactorAccountNumber] = @id");
                query.Append("SELECT @intErrorCode=@@ERROR;");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Transactions] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@TransactionAmount", CsType.Decimal, ParameterDirection.Input, entity.TransactionAmount);
                        DBHelper.AddParameter(dbCommand, "@TransactorAccountNumber", CsType.Int, ParameterDirection.Input, entity.TransactorAccountNumber);
                        DBHelper.AddParameter(dbCommand, "@RecieverAccountNumber", CsType.Int, ParameterDirection.Input, entity.RecieverAccountNumber);
                        DBHelper.AddParameter(dbCommand, "@TransactionDate", CsType.DateTime, ParameterDirection.Input, entity.TransactionDate);
                        DBHelper.AddParameter(dbCommand, "@isSuccess", CsType.Boolean, ParameterDirection.Input, entity.isSuccess);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Transactions] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                // TODO - ADD Logging Commons
                Console.WriteLine(ex.Message);
                throw new Exception("TransactionsRepository::Update:Error occured.", ex);
            }
        }
    }
}

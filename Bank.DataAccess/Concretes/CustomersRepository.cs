using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Bank.Commons;
using Bank.DataAccess.Abstractions;
using Bank.Models.Concretes;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace Bank.DataAccess.Concretes
{
    public class CustomersRepository : IRepository<Customers>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public CustomersRepository()
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
                query.Append("FROM [dbo].[tbl_Customers] ");
                query.Append("WHERE ");
                query.Append("[CustomerID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Customers] can't be null. ");

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
                                "Deleting Error for entity [tbl_Customer] reported the Database ErrorCode: " +
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
                throw new Exception("CustomersRepository::Insert:Error occured.", ex);
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

        public bool Insert(Customers entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Customers] ");
                query.Append("( [CustomerName], [CustomerSurname], [CustomerPasskey], [Balance], [BalanceType], [isActive] ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @CustomerName, @CustomerSurname, @CustomerPasskey, @Balance, @BalanceType, @IsActive ) ");
                query.Append("SELECT @intErrorCode=@@ERROR;");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if(dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Customers] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@CustomerName", CsType.String, ParameterDirection.Input, entity.CustomerName);
                        DBHelper.AddParameter(dbCommand, "@CustomerSurname", CsType.String, ParameterDirection.Input, entity.CustomerSurname);
                        DBHelper.AddParameter(dbCommand, "@CustomerPasskey", CsType.String, ParameterDirection.Input, entity.CustomerPasskey);
                        DBHelper.AddParameter(dbCommand, "@Balance", CsType.Decimal, ParameterDirection.Input, entity.Balance);
                        DBHelper.AddParameter(dbCommand, "@BalanceType", CsType.Byte, ParameterDirection.Input, entity.BalanceType);
                        DBHelper.AddParameter(dbCommand, "@IsActive", CsType.Boolean, ParameterDirection.Input, entity.isActive);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if(_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Customer] reported the Database ErrorCode: "+_errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                // TODO - ADD Logging Commons
                Console.WriteLine(ex.Message);
                throw new Exception("CustomersRepository::Insert:Error occured.", ex);
            }
        }

        public IList<Customers> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Customers> customers = new List<Customers>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[CustomerID], [CustomerName], [CustomerSurname], [CustomerPasskey], [Balance], [BalanceType], [isActive] ");
                query.Append("FROM [dbo].[tbl_Customers] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Customers] can't be null. ");

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
                                    var entity = new Customers();
                                    entity.CustomerID = reader.GetInt32(0);
                                    entity.CustomerName = reader.GetString(1);
                                    entity.CustomerSurname = reader.GetString(2);
                                    entity.CustomerPasskey = reader.GetString(3);
                                    entity.Balance = reader.GetDecimal(4);
                                    entity.BalanceType = reader.GetByte(5);
                                    entity.isActive = reader.GetBoolean(6);
                                    customers.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Customer] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return customers;
            }
            catch (Exception ex)
            {
                // TODO - ADD Logging Commons
                Console.WriteLine(ex.Message);
                throw new Exception("CustomersRepository::SelectAll:Error occured.", ex);
            }
        }

        public Customers SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Customers customer = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[CustomerID], [CustomerName], [CustomerSurname], [CustomerPasskey], [Balance], [BalanceType], [isActive] ");
                query.Append("FROM [dbo].[tbl_Customers] ");
                query.Append("WHERE ");
                query.Append("[CustomerID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Customers] can't be null. ");

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
                                    var entity = new Customers();
                                    entity.CustomerID = reader.GetInt32(0);
                                    entity.CustomerName = reader.GetString(1);
                                    entity.CustomerSurname = reader.GetString(2);
                                    entity.CustomerPasskey = reader.GetString(3);
                                    entity.Balance = reader.GetDecimal(4);
                                    entity.BalanceType = reader.GetByte(5);
                                    entity.isActive = reader.GetBoolean(6);
                                    customer = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Customer] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                customer.Transactions = new TranscationsRepository().SelectAll().Where(x=>x.TransactorAccountNumber.Equals(customer.CustomerID) || x.RecieverAccountNumber.Equals(customer.CustomerID)).ToList();
                return customer;
            }
            catch (Exception ex)
            {
                // TODO - ADD Logging Commons
                Console.WriteLine(ex.Message);
                throw new Exception("CustomersRepository::SelectById:Error occured.", ex);
            }
        }

        public bool Update(Customers entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Customers] ");
                query.Append(" SET [CustomerName] = @CustomerName, [CustomerSurname] = @CustomerSurname, [CustomerPasskey] =  @CustomerPasskey, [Balance] = @Balance, [BalanceType] = @BalanceType, [isActive] = @IsActive ");
                query.Append(" WHERE ");
                query.Append(" [CustomerID] = @CustomerID ");
                query.Append(" SELECT @intErrorCode = @@ERROR; ");

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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Customers] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@CustomerID", CsType.String, ParameterDirection.Input, entity.CustomerID);
                        DBHelper.AddParameter(dbCommand, "@CustomerName", CsType.String, ParameterDirection.Input, entity.CustomerName);
                        DBHelper.AddParameter(dbCommand, "@CustomerSurname", CsType.String, ParameterDirection.Input, entity.CustomerSurname);
                        DBHelper.AddParameter(dbCommand, "@CustomerPasskey", CsType.String, ParameterDirection.Input, entity.CustomerPasskey);
                        DBHelper.AddParameter(dbCommand, "@Balance", CsType.Decimal, ParameterDirection.Input, entity.Balance);
                        DBHelper.AddParameter(dbCommand, "@BalanceType", CsType.Byte, ParameterDirection.Input, entity.BalanceType);
                        DBHelper.AddParameter(dbCommand, "@IsActive", CsType.Boolean, ParameterDirection.Input, entity.isActive);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Customer] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                // TODO - ADD Logging Commons
                Console.WriteLine(ex.Message);
                throw new Exception("CustomersRepository::Update:Error occured.", ex);
            }
        }
    }
}

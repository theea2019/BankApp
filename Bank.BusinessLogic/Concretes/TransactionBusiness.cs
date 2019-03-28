using System;
using System.Collections.Generic;
using System.Linq;
using Bank.DataAccess.Concretes;
using Bank.Models.Concretes;


namespace Bank.BusinessLogic
{
    /// <summary>
    ///     <english>
    ///         This class opens our gates of Transactions to the real world and evolves our datas to informations and combines them with business rules that developed by customer.
    ///     </english>
    ///     <turkish>
    ///        Bu sınıf verilerimizi bilgiye dönüştürür ve onları müşterinin istediği iş kurallarıyla harmanlayıp Transaction işlemlerini dış dünyaya açtığımız yerdir. 
    ///     </turkish>
    /// </summary>
    public class TransactionBusiness : IDisposable
    {
        private CustomersBusiness _customerbusiness = new CustomersBusiness();
        private bool _bDisposed;
        private readonly object _lock = new object();
        private Transactions generalTransaction;

        private bool InsertTransaction(Transactions entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new TranscationsRepository())
                {
                    isSuccess = repo.Insert(entity);
                    using (var customerRepo = new CustomersRepository())
                    {
                        Customers c = customerRepo.SelectedById(entity.TransactorAccountNumber);
                        generalTransaction = c.Transactions.ElementAt(c.Transactions.Count-1);
                    }
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:TransactionBusiness::InsertTransaction::Error occured.", ex);
            }
        }

        public List<Transactions> SelectAllTransactions()
        {
            var responseEntities = new List<Transactions>();

            try
            {
                using (var repo = new TranscationsRepository())
                {
                    foreach (var entity in repo.SelectAll())
                    {
                        responseEntities.Add(entity);
                    }
                }
                return responseEntities;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool MakeTransaction(Transactions transaction, Customers sender, Customers reciever)
        {
            try
            {
                bool isSuccess = false;
                transaction.isSuccess = false;
                if (sender.Balance > transaction.TransactionAmount)
                {
                    lock (_lock)
                        sender.Balance -= transaction.TransactionAmount;
                    
                    lock (_lock)                    
                        reciever.Balance += transaction.TransactionAmount;

                    isSuccess = InsertTransaction(transaction);
                    if (isSuccess != transaction.isSuccess)
                    {
                        transaction = generalTransaction;
                        transaction.isSuccess = isSuccess;
                        if (UpdateTransactionInfo(transaction))
                        {
                            lock (_lock)
                                _customerbusiness.UpdateCustomer(sender);

                            lock (_lock)
                                _customerbusiness.UpdateCustomer(reciever);
                        }
                    }
                }

                return isSuccess;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:TransactionBusiness::MakeTransaction::Error occured.", ex);
            }
        }

        public bool WithdrawMoney(Transactions transaction, Customers customer)
        {
            try
            {
                bool isSuccess = false;
                transaction.isSuccess = false;
                if (customer.Balance > transaction.TransactionAmount)
                {
                    lock (_lock)                    
                        customer.Balance -= transaction.TransactionAmount;
                    
                    isSuccess = InsertTransaction(transaction);
                    if (isSuccess != transaction.isSuccess)
                    {
                        transaction = generalTransaction;
                        transaction.isSuccess = isSuccess;
                        if (UpdateTransactionInfo(transaction))
                            lock (_lock)
                                _customerbusiness.UpdateCustomer(customer);
                    }
                }

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:TransactionBusiness::WithdrawMoney::Error occured.", ex);
            }
        }

        public bool DepositMoney(Transactions transaction, Customers customer)
        {
            try
            {
                bool isSuccess = false;
                lock (_lock)
                    customer.Balance += transaction.TransactionAmount;

                isSuccess = InsertTransaction(transaction);
                if (isSuccess && isSuccess != transaction.isSuccess)
                {
                    transaction = generalTransaction;
                    transaction.isSuccess = isSuccess;
                    if (UpdateTransactionInfo(transaction))
                        lock (_lock)
                            _customerbusiness.UpdateCustomer(customer);
                }

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:TransactionBusiness::WithdrawMoney::Error occured.", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            // Check the Dispose method called before.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Clean the resources used.
                    _customerbusiness = null;
                }

                _bDisposed = true;
            }
        }

        private bool UpdateTransactionInfo(Transactions transaction)
        {
            try
            {
                bool isSuccess;
                using (var repo = new TranscationsRepository())
                {
                    isSuccess = repo.Update(transaction);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:TransactionBusiness::UpdateTransaction::Error occured.", ex);
            }
        }

        public TransactionBusiness()
        {
            _customerbusiness = new CustomersBusiness();
        }

    }
}


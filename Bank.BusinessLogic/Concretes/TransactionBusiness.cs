using System;
using System.Collections.Generic;
using Bank.DataAccess.Concretes;
using Bank.Models.Concretes;


namespace Bank.BusinessLogic
{
    public class TransactionBusiness : IDisposable
    {
        private CustomersBusiness _customerbusiness = new CustomersBusiness();
        private bool _bDisposed;
        private readonly object _lock = new object();

        public bool InsertTransaction(Transactions entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new TranscationsRepository())
                {
                    isSuccess = repo.Insert(entity);
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
                bool isSuccess;
                if (sender.Balance > transaction.TransactionAmount)
                {
                    lock (_lock)
                        sender.Balance -= transaction.TransactionAmount;
                    
                    lock (_lock)                    
                        reciever.Balance += transaction.TransactionAmount;

                    lock (_lock)
                        _customerbusiness.UpdateCustomer(sender);

                    lock (_lock)
                        _customerbusiness.UpdateCustomer(reciever);
                    
                    isSuccess = InsertTransaction(transaction);
                }
                else
                    return false;

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
                bool isSuccess;
                if (customer.Balance > transaction.TransactionAmount)
                {
                    lock (_lock)                    
                        customer.Balance -= transaction.TransactionAmount;

                    lock (_lock)
                        _customerbusiness.UpdateCustomer(customer);
                    
                    isSuccess = InsertTransaction(transaction);
                }
                else
                    return false;

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

        public TransactionBusiness()
        {
            _customerbusiness = new CustomersBusiness();
        }
    }
}


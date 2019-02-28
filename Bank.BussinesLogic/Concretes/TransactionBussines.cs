using System;
using System.Collections.Generic;
using Bank.DataAccess.Concretes;
using Bank.Models.Concretes;


namespace Bank.BussinessLogic
{
    public class TransactionBussines : IDisposable
    {
        private readonly CustomersBussiness customerbussiness = new CustomersBussiness();
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
                        customerbussiness.UpdateCustomer(sender);

                    lock (_lock)
                        customerbussiness.UpdateCustomer(reciever);
                    
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
                        customerbussiness.UpdateCustomer(customer);
                    
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
            GC.SuppressFinalize(true);
        }

        public TransactionBussines()
        {
            // TODO
        }
    }
}


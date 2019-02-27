using System;
using System.Collections.Generic;
using Bank.DataAccess.Concretes;
using Bank.Models.Concretes;

namespace Bank.BussinessLogic
{
    public class CustomersBussiness : IDisposable
    {
        // TODO: LoggingHandler

        public bool InsertCustomer(Customers entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CustomersRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                // TODO: LoggingException
                throw new Exception("BusinessLogic:CustomerBusiness::InsertCustomer::Error occured.", ex);
            }
        }

        public bool UpdateCustomer(Customers entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CustomersRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                // TODO: LoggingException
                throw new Exception("BusinessLogic:CustomerBusiness::UpdateCustomer::Error occured.", ex);
            }
        }

        public bool DeleteCustomer(Customers entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CustomersRepository())
                {
                    isSuccess = repo.DeletedById(entity.CustomerID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                // TODO: LoggingException
                throw new Exception("BusinessLogic:CustomerBusiness::DeleteCustomer::Error occured.", ex);
            }
        }
        
        public Customers SelectCustomerById (int customerId)
        {
            try
            {
                Customers responseEntitiy;
                using (var repo = new CustomersRepository())
                {
                    responseEntitiy = repo.SelectedById(customerId);
                    // TODO: response != null
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                // Log Error
                throw;
            }
        }

        public List<Customers> SelectAllCustomers()
        {
            var responseEntities = new List<Customers>();

            try
            {
                using (var repo = new CustomersRepository())
                {
                    foreach (var entity in repo.SelectAll())
                    {
                        responseEntities.Add(entity);
                    }
                }
                return responseEntities;
            }
            catch (Exception ex)
            {
                // Log Error
                throw;
            }
        }

        public CustomersBussiness()
        {
            // TODO : LogHandler inital object.
        }

            //TODO : Dispose
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}


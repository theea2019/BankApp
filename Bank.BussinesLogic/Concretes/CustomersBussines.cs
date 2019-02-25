using System;
using System.Collections.Generic;

namespace Bank.BussinessLogic
{
    public class CustomersBussiness : IDisposable
    {
        // TODO: LoggingHandler

        public bool InsertCustomer(CustomerEntitiy entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CustomerRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                // TODO: LoggingException
                throw;
            }
        }

        public bool UpdateCustomer(CustomerEntitiy entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CustomerRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                // TODO: LoggingException
                throw;
            }
        }

        public bool DeleteCustomer(CustomerEntitiy entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CustomerRepository())
                {
                    isSuccess = repo.Delete(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                // TODO: LoggingException
                throw;
            }
        }
        
        public CustomerEntitiy SelectCustomerById (int customerId)
        {
            try
            {
                CustomerEntitiy responseEntitiy;
                using (var repo = new CustomerRepository())
                {
                    responseEntitiy = repo.SelectById(customerId);
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

        public List<CustomerEntity> SelectAllCustomers()
        {
            var responseEntities = new List<CustomerEntitiy>();

            try
            {
                using (var repo = new CustomerRepository())
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
    }
}


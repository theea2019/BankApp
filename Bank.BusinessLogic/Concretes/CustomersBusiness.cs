using System;
using System.Collections.Generic;
using Bank.Commons.Concretes.Helpers;
using Bank.Commons.Concretes.Logger;
using Bank.DataAccess.Concretes;
using Bank.Models.Concretes;

namespace Bank.BusinessLogic
{
    public class CustomersBusiness : IDisposable
    {
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
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
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
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::UpdateCustomer::Error occured.", ex);
            }
        }

        public bool DeleteCustomerById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CustomersRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
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
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Customer doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::SelectCustomerById::Error occured.", ex);
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
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::SelectAllCustomers::Error occured.", ex);
            }
        }

        public CustomersBusiness()
        {
            //Auto-generated Code   
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}


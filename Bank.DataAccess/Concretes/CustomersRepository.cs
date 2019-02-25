using System;
using System.Collections.Generic;
using Bank.DataAccess;
using Bank.DataAccess.Abstractions;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace Bank.DataAccess.Concretes
{
    public class CustomersRepository : IRepository<Class1> IDisposable
    {
        public CustomersRepository()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public bool DeletedById(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Class1 entity)
        {
            throw new NotImplementedException();
        }

        public IList<Class1> SelectAll()
        {
            throw new NotImplementedException();
        }

        public Class1 SelectedById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Class1 entity)
        {
            throw new NotImplementedException();
        }
    }
}

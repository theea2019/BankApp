using System;
using System.Collections.Generic;
using Bank.DataAccess.Abstractions;
using Bank.Models.Concretes;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace Bank.DataAccess.Concretes
{
    public class CustomersRepository : IRepository<Customers>, IDisposable
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
            GC.SuppressFinalize(this);
        }

        public bool Insert(Customers entity)
        {
            throw new NotImplementedException();
        }

        public IList<Customers> SelectAll()
        {
            throw new NotImplementedException();
        }

        public Customers SelectedById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Customers entity)
        {
            throw new NotImplementedException();
        }
    }
}

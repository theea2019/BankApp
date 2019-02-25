using System;
using System.Collections.Generic;

/// <summary>
/// This Interface contains common methods implemented by all repositories.
/// </summary>
namespace Bank.DataAccess.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class
    {
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        bool DeletedById(int id);
        TEntity SelectedById(int id);
        IList<TEntity> SelectAll();
    }
}

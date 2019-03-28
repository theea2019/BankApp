using System;
using System.Collections.Generic;

/// <summary>
///     <english>
///         This is a generic Interface that contains common methods implemented by all repositories.
///     </english>
///     <turkish>
///         Bu jenerik arayüz repository deseninin gerçeklenmesi için tüm varlıkların repository'leri için kalıtılmalı ve gerçeklenmelidir.
///     </turkish>
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

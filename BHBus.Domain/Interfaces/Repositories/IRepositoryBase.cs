using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BHBus.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity Add(TEntity obj);

        TEntity GetById(Guid id);
        
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAllAsNoTracking();

        void Update(TEntity obj);

        void Remove(TEntity obj);

        void Dispose();
    }
}

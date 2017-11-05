using System;
using System.Collections.Generic;

namespace BHBus.Application.Interfaces
{
    public interface IAppServiceBase<TEntity> where TEntity : class
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

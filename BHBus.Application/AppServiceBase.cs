using BHBus.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BHBus.Application
{
    public class AppServiceBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public AppServiceBase(IRepositoryBase<TEntity> repository)
        {
            this._repository = repository;         
        }

        public TEntity Add(TEntity obj)
        {
            return this._repository.Add(obj);
        }

        public void Dispose()
        {
            this._repository.Dispose();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this._repository.GetAll();
        }

        public IEnumerable<TEntity> GetAllAsNoTracking()
        {
            return this._repository.GetAllAsNoTracking();
        }

        public TEntity GetById(Guid id)
        {
            return this._repository.GetById(id);
        }

        public void Remove(TEntity obj)
        {
            this._repository.Remove(obj);
        }

        public void Update(TEntity obj)
        {
            this._repository.Update(obj);
        }
    }
}

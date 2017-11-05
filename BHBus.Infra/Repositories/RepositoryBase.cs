using BHBus.Domain.Interfaces;
using BHBus.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BHBus.Infra.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected BHBusContext Db = new BHBusContext();

        public TEntity Add(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
            Db.SaveChanges();

            return obj;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetAllAsNoTracking()
        {
            return Db.Set<TEntity>().AsNoTracking().ToList();
        }

        public TEntity GetById(Guid id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity obj)
        {
            Db.Set<TEntity>().Remove(obj);
        }

        public void Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> FindAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return Db.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate);
        }
    }
}

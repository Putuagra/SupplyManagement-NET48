using SupplyManagement_NET48.Contracts;
using SupplyManagement_NET48.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SupplyManagement_NET48.Repositories
{
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class
    {
        protected readonly SupplyManagementDbContext Context;

        public GeneralRepository(SupplyManagementDbContext context)
        {
            Context = context;
        }

        public ICollection<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public TEntity GetByGuid(Guid guid)
        {
            var entity = Context.Set<TEntity>().Find(guid);
            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public TEntity Create(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Add(entity);
                Context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(TEntity entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Remove(entity);
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
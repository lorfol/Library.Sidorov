using Library.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Library.Infrastructure.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly LibraryDbContext context;

        protected readonly DbSet<TEntity> entities;

        public BaseRepository(LibraryDbContext context)
        {
            this.context = context;
            this.entities = context.Set<TEntity>();
        }

        public virtual TEntity Create(TEntity item)
        {
            var entity = this.entities.Add(item);
            return entity;
        }

        public virtual void Delete(TEntity item)
        {
            this.entities.Remove(item);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.entities.Where(predicate);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.entities.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return this.entities.Find(id);
        }

        public virtual void Update(object id, TEntity item)
        {
            var entity = this.entities.Find(id);
            if (entity == null)
            {
                return;
            }

            this.context.Entry(entity).CurrentValues.SetValues(item);
        }
    }
}

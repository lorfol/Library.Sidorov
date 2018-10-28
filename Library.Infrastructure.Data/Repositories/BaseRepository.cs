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

        public void Create(TEntity item)
        {
            this.entities.Add(item);
        }

        public void Delete(TEntity item)
        {
            this.entities.Remove(item);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.entities.Where(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.entities.ToList();
        }

        public TEntity GetById(int id)
        {
            return this.entities.Find(id);
        }

        public void Update(int id, TEntity item)
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

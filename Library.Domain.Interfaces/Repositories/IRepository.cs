﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Library.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(object id);

        TEntity Create(TEntity item);

        void Update(object id, TEntity item);

        void Delete(TEntity item);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}

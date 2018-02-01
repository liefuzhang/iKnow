using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace iKnow.Core.Repositories {
    public interface IRepository<TEntity> where TEntity : class {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetById(int id);

        IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity SingleOrDefault(
            Expression<Func<TEntity, bool>> predicate,
            string includeProperties = "");

        TEntity Single(
            Expression<Func<TEntity, bool>> predicate,
            string includeProperties = "");

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
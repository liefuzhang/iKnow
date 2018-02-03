using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace iKnow.Core.Repositories {
    public interface IRepository<TEntity> where TEntity : class {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        TEntity GetById(int id);

        TEntity SingleOrDefault(
            Expression<Func<TEntity, bool>> filter,
            string includeProperties = null);

        TEntity Single(
            Expression<Func<TEntity, bool>> filter,
            string includeProperties = null);

        TEntity FirstOrDefault(
            Expression<Func<TEntity, bool>> filter,
            string includeProperties = null);

        TEntity First(
            Expression<Func<TEntity, bool>> filter,
            string includeProperties = null);

        int Count(Expression<Func<TEntity, bool>> filter);
        bool Any(Expression<Func<TEntity, bool>> filter);
        bool All(Expression<Func<TEntity, bool>> filter);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
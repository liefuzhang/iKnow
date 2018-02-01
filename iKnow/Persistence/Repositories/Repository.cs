using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using iKnow.Core.Repositories;

namespace iKnow.Persistence.Repositories {
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class {
        protected readonly DbContext Context;
        private DbSet<TEntity> _dbSet;

        public Repository(DbContext context) {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        // refer to https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "") {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null) {
                query = query.Where(filter);
            }

            query = AddIncludeProperty(includeProperties, query);

            if (orderBy != null) {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public TEntity GetById(int id) {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "") {
            return Get(filter: null, orderBy: orderBy, includeProperties: includeProperties);
        }

        public TEntity SingleOrDefault(
            Expression<Func<TEntity, bool>> predicate,
            string includeProperties = "") {
            IQueryable<TEntity> query = _dbSet;
            query = AddIncludeProperty(includeProperties, query);
            return query.SingleOrDefault(predicate);
        }

        public TEntity Single(
            Expression<Func<TEntity, bool>> predicate,
            string includeProperties = "") {
            IQueryable<TEntity> query = _dbSet;
            query = AddIncludeProperty(includeProperties, query);
            return query.Single(predicate);
        }

        public void Add(TEntity entity) {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities) {
            _dbSet.AddRange(entities);
        }

        public void Remove(TEntity entity) {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities) {
            _dbSet.RemoveRange(entities);
        }

        // Helper methods
        private static IQueryable<TEntity> AddIncludeProperty(string includeProperties, IQueryable<TEntity> query) {
            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                query = query.Include(includeProperty);
            }
            return query;
        }
    }
}

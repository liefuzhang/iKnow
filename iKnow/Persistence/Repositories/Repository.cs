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
        private readonly DbSet<TEntity> _dbSet;
        private IQueryable<TEntity> _query;

        public Repository(DbContext context) {
            Context = context;
            _dbSet = Context.Set<TEntity>();
            _query = _dbSet;
        }

        // refer to https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application

        private IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null) {
            includeProperties = includeProperties ?? "";

            if (filter != null) {
                _query = _query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                _query = _query.Include(includeProperty);
            }

            if (orderBy != null) {
                _query = orderBy(_query);
            }

            if (skip.HasValue) {
                _query = _query.Skip(skip.Value);
            }

            if (take.HasValue) {
                _query = _query.Take(take.Value);
            }

            return _query;
        }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null) {
            return GetQueryable(filter, orderBy, includeProperties, skip, take).ToList();
        }

        public IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null) {
            return GetQueryable(null, orderBy, includeProperties, skip, take).ToList();
        }

        public TEntity GetById(int id) {
            return _dbSet.Find(id);
        }

        public TEntity SingleOrDefault(
            Expression<Func<TEntity, bool>> filter,
            string includeProperties = null) {
            return GetQueryable(filter, null, includeProperties).SingleOrDefault();
        }

        public TEntity Single(
            Expression<Func<TEntity, bool>> filter,
            string includeProperties = null) {
            return GetQueryable(filter, null, includeProperties).Single();
        }

        public TEntity FirstOrDefault(
            Expression<Func<TEntity, bool>> filter,
            string includeProperties = null) {
            return GetQueryable(filter, null, includeProperties).FirstOrDefault();
        }

        public TEntity First(
            Expression<Func<TEntity, bool>> filter,
            string includeProperties = null) {
            return GetQueryable(filter, null, includeProperties).First();
        }

        public int Count(Expression<Func<TEntity, bool>> filter) {
            return GetQueryable(filter).Count();
        }

        public bool Any(Expression<Func<TEntity, bool>> filter) {
            return GetQueryable(filter).Any();
        }

        public bool All(Expression<Func<TEntity, bool>> filter) {
            return _query.All(filter);
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
    }
}

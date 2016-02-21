using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.BulkInsert.Extensions;
using System.Data;

namespace AxadoTest.Data.Infrastructure
{
    public class RepositoryBase<T> : IRepository<T>
        where T : class
    {
        private DataContext dataContext;
        protected IDbSet<T> dbSet;

        public IDbSet<T> GetDbSet()
        {
             return dbSet;
        }

        protected RepositoryBase(IDatabaseFactory databasefactory)
        {
            DatabaseFactory = databasefactory;
            dbSet = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        public DataContext DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }

        }

        public virtual T Add(T entity)
        {
            return dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            DataContext.BulkInsert(entities);
        }

        public virtual RepositoryBase<T> Update(T entity)
        {
            dbSet.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
            return this;
        }

        public virtual RepositoryBase<T> Delete(T entity)
        {
            if(entity != null) { 
                dbSet.Remove(entity);
            }
            return this;
        }

        public virtual RepositoryBase<T> DeleteRange(IEnumerable<T> entities)
        {
            DataContext.Set<T>().RemoveRange(entities);
            return this;
        }

        public RepositoryBase<T> DeleteAll()
        {
            var tabela = (dataContext as IObjectContextAdapter).ObjectContext.CreateObjectSet<T>().EntitySet.Name;
            string query = "DELETE FROM " + tabela;
            dataContext.Database.ExecuteSqlCommand(query);
            return this;
        }

        public RepositoryBase<T> DeleteAll(string where, List<Object> parameters)
        {
            var tabela = (dataContext as IObjectContextAdapter).ObjectContext.CreateObjectSet<T>().EntitySet.Name;
            string query = "DELETE FROM " + tabela;

            if (!string.IsNullOrEmpty(where))
            {
                query += " " + where;
            }

            if (parameters == null)
            {
                dataContext.Database.ExecuteSqlCommand(query);
            }
            else
            {
                dataContext.Database.ExecuteSqlCommand(query,parameters.ToArray());
            }
            
            return this;
        }

        public virtual RepositoryBase<T> Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
            return this;
        }

        public virtual T GetById(long id)
        {
            var entity = dbSet.Find(id);
            return entity;
        }

        public virtual T GetById(string id)
        {
            var entity = dbSet.Find(id);
            DataContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = dbSet;
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable.ToList();
        }

        public IEnumerable<T> GetAllExcept(IEnumerable<T> entities)
        {
            return dbSet.Except(entities).ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public virtual IEnumerable<T> GetManyIncluding(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = dbSet;
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable.Where(where).ToList();
        }

        public IEnumerable<T> GetTop(int top)
        {
            return dbSet.Take(top).ToList();
        }

        public IEnumerable<TResult> GetWithJoin<TInner, TKey, TResult>(IEnumerable<TInner> inner, Expression<Func<T, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<T, TInner, TResult>> resultSelector)
        {
            var query = dbSet.Join(
                inner,
                outerKeySelector,  //FK
                innerKeySelector,  //PK
                resultSelector);

            var result = query.ToList();

            return result;
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        public void Commit()
        {
            DataContext.SaveChanges();
        }

        public bool Exists(long id)
        {
            return dbSet.Any();
        }

        public void Detach(T entity)
        {
            DataContext.Entry(entity).State = EntityState.Detached;
        }

        public long GetCount()
        {
            return dbSet.Count();
        }
    }
}

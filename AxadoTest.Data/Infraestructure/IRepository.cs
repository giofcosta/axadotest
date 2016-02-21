using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace AxadoTest.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IDbSet<T> GetDbSet();
        T Add(T entity);
        void AddRange(IEnumerable<T> entities);
        RepositoryBase<T> Update(T entity);
        RepositoryBase<T> Delete(T entity);
        RepositoryBase<T> DeleteRange(IEnumerable<T> entity);
        RepositoryBase<T> DeleteAll();
        RepositoryBase<T> DeleteAll(string where, List<Object> parameters);
        RepositoryBase<T> Delete(Expression<Func<T, bool>> where);
        T GetById(long id);
        T GetById(string id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllExcept(IEnumerable<T> entities);
        IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        IEnumerable<T> GetManyIncluding(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetTop(int top);
        IEnumerable<TResult> GetWithJoin<TInner, TKey, TResult>(IEnumerable<TInner> inner, Expression<Func<T, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<T, TInner, TResult>> resultSelector);
        long GetCount();
        void Detach(T entity);
        void Commit();
        bool Exists(long id);
    }
}

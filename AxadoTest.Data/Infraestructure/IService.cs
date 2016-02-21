using System;
using System.Collections.Generic;

namespace AxadoTest.Data.Infrastructure
{
    public interface IService<T>
        where T : class
    {
        T GetById(long id);
        IEnumerable<T> GetAll();
        long GetCount();
        IEnumerable<T> Search(string term);
        T Add(T viewModel);
        void AddRange(IEnumerable<T> models);
        void Update(T viewModel);
        void Delete(long id);
        void DeleteRange(IEnumerable<T> models);
        void DeleteAll();
        void DeleteAll(string where, List<Object> parameters);
    }
}

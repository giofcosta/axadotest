using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;

namespace AxadoTest.Data.Infrastructure
{
    public abstract class ServiceBase<TModel, TViewModel> : IService<TViewModel>
        where TModel : class
        where TViewModel : class
    {
        public IRepository<TModel> Repository { get; set; }

        public virtual Expression<Func<TModel, bool>> SearchQuery
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private string searchTerm;
        public string SearchTerm
        {
            get
            {
                return searchTerm;
            }
            set
            {
                if (value == null)
                {
                    searchTerm = string.Empty;
                }
                else
                {
                    searchTerm = value; 
                }
            }
        }

        public ServiceBase(IRepository<TModel> repository)
        {
            this.Repository = repository;
        }

        public virtual TViewModel GetById(long id)
        {
            var model = Repository.GetById(id);
            var viewModel = Mapper.Map<TModel, TViewModel>(model);
            Repository.Detach(model);
            return viewModel;
        }

        public virtual IEnumerable<TViewModel> GetAll()
        {
            var models = Repository.GetAll();
            var viewmodels = Mapper.Map<IEnumerable<TModel>, List<TViewModel>>(models);
            return viewmodels;
        }

        public virtual IEnumerable<TViewModel> Search(string text)
        {
            this.SearchTerm = text;
            var models = (!string.IsNullOrEmpty(text)) 
                ? Repository.GetMany(SearchQuery) 
                : Repository.GetAll();
            var viewmodels = Mapper.Map<IEnumerable<TModel>, List<TViewModel>>(models);
            return viewmodels;
        }

        public virtual TViewModel Add(TViewModel viewModel)
        {
            var model = Mapper.Map<TViewModel, TModel>(viewModel);
            var modelback =  Repository.Add(model);
            Repository.Commit();
            return Mapper.Map<TModel, TViewModel>(modelback);
        }

        public void AddRange(IEnumerable<TViewModel> models)
        {
            var model = Mapper.Map<IEnumerable<TViewModel>, IEnumerable<TModel>>(models);
            Repository.AddRange(model);
            Repository.Commit();
        }

        public virtual void Update(TViewModel viewModel)
        {
            var model = Mapper.Map<TViewModel, TModel>(viewModel);
            Repository.Update(model);
            Repository.Commit();
        }

        public virtual void Delete(long id)
        {
            try
            {
                var model = Repository.GetById(id);
                Repository.Delete(model);
                Repository.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteRange(IEnumerable<TViewModel> models)
        {
            var model = Mapper.Map<IEnumerable<TViewModel>, IEnumerable<TModel>>(models);

            Repository.DeleteRange(model);
            Repository.Commit();
        }

        public virtual void DeleteAll()
        {
            Repository.DeleteAll();
            Repository.Commit();
        }

        public virtual void DeleteAll(string where, List<Object> parameters)
        {
            Repository.DeleteAll(where, parameters);
            Repository.Commit();
        }

        public long GetCount()
        {
            var registers = Repository.GetCount();
            return registers;
        }        
    }
}

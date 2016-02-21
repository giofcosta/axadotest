﻿namespace AxadoTest.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly IDatabaseFactory databaseFactory;
        private DataContext dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected DataContext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Commit()
        {
            DataContext.SaveChanges();
        }
    }
}

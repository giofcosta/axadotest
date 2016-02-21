namespace AxadoTest.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private DataContext dataContext;
        public DataContext Get()
        {
            return dataContext ?? (dataContext = new DataContext());
        }

        protected override void DiposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}

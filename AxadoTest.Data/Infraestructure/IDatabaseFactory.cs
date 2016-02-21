using System;

namespace AxadoTest.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        DataContext Get();
    }
}

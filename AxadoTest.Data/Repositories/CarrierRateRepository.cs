using AxadoTest.Data.Infrastructure;
using AxadoTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxadoTest.Data.Repositories
{
    public class CarrierRateRepository : RepositoryBase<CarrierRate>, ICarrierRateRepository
    {
        public CarrierRateRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}

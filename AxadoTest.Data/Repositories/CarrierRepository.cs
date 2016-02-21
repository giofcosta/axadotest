using AxadoTest.Data.Infrastructure;
using AxadoTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxadoTest.Data.Repositories
{
    public class CarrierRepository : RepositoryBase<Carrier>, ICarrierRepository
    {
        public CarrierRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}

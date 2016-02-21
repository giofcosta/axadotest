using AxadoTest.Data.Infrastructure;
using AxadoTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxadoTest.Data.Services
{
    public interface ICarrierRateService : IService<CarrierRateViewModel>
    {
        IEnumerable<CarrierRateViewModel> GetAllForUser(string userId);
    }
}

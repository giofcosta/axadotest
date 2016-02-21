using AxadoTest.Data.Infrastructure;
using AxadoTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxadoTest.Data.Services
{
    public interface ICarrierService : IService<CarrierViewModel>
    {
        IEnumerable<CarrierViewModel> GetAllCanRate(string userId);
    }
}

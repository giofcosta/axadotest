using AutoMapper;
using AxadoTest.Data.Infrastructure;
using AxadoTest.Data.Repositories;
using AxadoTest.Model;
using AxadoTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxadoTest.Data.Services
{
    public class CarrierService : ServiceBase<Carrier, CarrierViewModel>, ICarrierService
    {
        public CarrierService(ICarrierRepository repository) : base(repository)
        {

        }

        public IEnumerable<CarrierViewModel> GetAllCanRate(string userId)
        {
            var models = Repository.GetMany(x => !x.Rates.Any(y => y.UserId == userId));
            var viewmodels = Mapper.Map<IEnumerable<Carrier>, List<CarrierViewModel>>(models);
            return viewmodels;
        }
    }
}

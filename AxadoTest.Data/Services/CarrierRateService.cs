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
    public class CarrierRateService : ServiceBase<CarrierRate, CarrierRateViewModel>, ICarrierRateService
    {
        public CarrierRateService(ICarrierRateRepository repository) : base(repository)
        {

        }

        public IEnumerable<CarrierRateViewModel> GetAllForUser(string userId)
        {
            var models = Repository.GetMany(x => x.UserId == userId);
            var viewmodels = Mapper.Map<IEnumerable<CarrierRate>, List<CarrierRateViewModel>>(models);
            return viewmodels;
        }
    }
}

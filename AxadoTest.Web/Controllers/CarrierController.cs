using AxadoTest.Api.Infrastructure;
using AxadoTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AxadoTest.Data.Infrastructure;

namespace AxadoTest.Web.Controllers
{
    [Authorize]
    public class CarrierController : CrudController<CarrierViewModel>
    {
        public CarrierController(IService<CarrierViewModel> service) : base(service)
        {

        }
    }
}
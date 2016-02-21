using AutoMapper;
using AxadoTest.Model;
using AxadoTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxadoTest.Data
{
    public static class MapConfig
    {
        public static void Register()
        {
            //DB -> ViewModel
            Mapper.CreateMap<Carrier, CarrierViewModel>();
            Mapper.CreateMap<CarrierRate, CarrierRateViewModel>();

            //ViewModel -> DB
            Mapper.CreateMap<CarrierViewModel, Carrier>();
            Mapper.CreateMap<CarrierRateViewModel, CarrierRate>();
        }
    }
}

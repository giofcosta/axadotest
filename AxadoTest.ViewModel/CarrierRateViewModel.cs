using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxadoTest.ViewModel
{
    public class CarrierRateViewModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long CarrierId { get; set; }
        public CarrierViewModel Carrier { get; set; }
        public int Rate { get; set; }
    }
}

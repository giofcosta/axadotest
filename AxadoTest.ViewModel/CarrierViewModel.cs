using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxadoTest.ViewModel
{
    public class CarrierViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Average {
            get
            {
                if(Rates != null && Rates.Any())
                {
                    return (int)Math.Round(Rates.Select(x => x.Rate).Average());
                }

                return 0;
            }
        }

        public IEnumerable<CarrierRateViewModel> Rates { get; set; }
        public bool Active { get; set; }
    }
}

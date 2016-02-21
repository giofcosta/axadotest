using AxadoTest.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxadoTest.Data.Configurations
{
    public class CarrierConfiguration : EntityTypeConfiguration<Carrier>
    {
        public CarrierConfiguration()
        {
            HasMany(x => x.Rates)
                .WithRequired(x => x.Carrier)
                .WillCascadeOnDelete(true);
        }
    }
}

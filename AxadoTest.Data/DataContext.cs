using AxadoTest.Data.Configurations;
using AxadoTest.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxadoTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
           : base("DefaultConnection")
        {
            this.Database.CommandTimeout = 300;
        }

        public virtual DbSet<Carrier> Carrier { get; set; }
        public virtual DbSet<CarrierRate> CarrierRate { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CarrierConfiguration());
            modelBuilder.Configurations.Add(new CarrierRateConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.Entity is BaseEntity)
                {
                    if (entity.State == EntityState.Added)
                    {
                        ((BaseEntity)entity.Entity).Created = DateTime.Now;
                        ((BaseEntity)entity.Entity).Active = true;
                    }
                    else
                    {
                        ((BaseEntity)entity.Entity).Created = entity.GetDatabaseValues().GetValue<DateTime>("Created");
                    }

                    ((BaseEntity)entity.Entity).Updated = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}

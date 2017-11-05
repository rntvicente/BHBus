using BHBus.Domain.Entities;
using BHBus.Infra.EntityConfig;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace BHBus.Infra.Context
{
    public class BHBusContext : DbContext
    {
        public BHBusContext()
            : base("BHBusContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Passenger> Passengeres { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<BusLine> BusLines { get; set; }

        public DbSet<Balance> Balances { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "ID")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new PassengerConfiguration());
            modelBuilder.Configurations.Add(new CardConfiguration());
            modelBuilder.Configurations.Add(new BusLineConfiguration());
            modelBuilder.Configurations.Add(new BalanceConfiguration());

            // Configure PassengerID como FK para Card 
            modelBuilder.Entity<Passenger>()
                        .HasRequired(p => p.Card)
                        .WithRequiredPrincipal(c => c.Passenger);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DateRegister") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DateRegister").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DateRegister").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Active") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Active").CurrentValue = true;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("BusLineID") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("BusLineID").CurrentValue = Guid.NewGuid();
                }
            }

            return base.SaveChanges();
        }
    }
}

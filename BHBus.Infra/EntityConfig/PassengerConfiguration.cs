using BHBus.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace BHBus.Infra.EntityConfig
{
    public class PassengerConfiguration : EntityTypeConfiguration<Passenger>
    {
        public PassengerConfiguration()
        {
            HasKey(p => p.PassengerID);

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            Property(p => p.Email.Address)
                .IsRequired()
                .HasMaxLength(254);

            Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(8);
        }
    }
}

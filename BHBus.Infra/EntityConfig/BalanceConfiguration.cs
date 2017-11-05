using BHBus.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace BHBus.Infra.EntityConfig
{
    public class BalanceConfiguration : EntityTypeConfiguration<Balance>
    {
        public BalanceConfiguration()
        {
            HasKey(s => new { s.PassengerID, s.DateRegister });

            Property(s => s.Value)
                .IsRequired();

            Property(s => s.BusLineID)
                .IsOptional();

            Property(s => s.TransactionType)
                .IsRequired()
                .HasMaxLength(1);
        }
    }
}

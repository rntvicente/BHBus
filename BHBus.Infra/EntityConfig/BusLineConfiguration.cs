using BHBus.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BHBus.Infra.EntityConfig
{
    public class BusLineConfiguration : EntityTypeConfiguration<BusLine>
    {
        public BusLineConfiguration()
        {
            HasKey(l => l.BusLineID);

            Property(l => l.Line)
                .IsRequired()
                .HasMaxLength(9);
        }
    }
}

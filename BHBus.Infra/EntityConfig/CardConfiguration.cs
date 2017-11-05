using BHBus.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BHBus.Infra.EntityConfig
{
    public class CardConfiguration : EntityTypeConfiguration<Card>
    {
        public CardConfiguration()
        {
            HasKey(c => c.PassengerID);

            Property(c => c.NumberCard)
                .IsRequired();
        }
    }
}

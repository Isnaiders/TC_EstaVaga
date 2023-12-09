using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TCC_API.Models.EntitiesConfig.Parking
{
    public class ParkingConfig : IEntityTypeConfiguration<Entities.Parking.Parking>
    {
        public void Configure(EntityTypeBuilder<Entities.Parking.Parking> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever().IsRequired().HasColumnName("ParkingId");
        }
    }
}

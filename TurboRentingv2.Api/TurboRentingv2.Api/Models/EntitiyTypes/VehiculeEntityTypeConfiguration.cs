using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurboRentingv2.Api.Models.Entities;

namespace TurboRentingv2.Api.Models.EntitiyTypes
{
    public class VehiculeEntityTypeConfiguration : IEntityTypeConfiguration<Vehicule>
    {
        public void Configure(EntityTypeBuilder<Vehicule> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Registration).IsRequired().HasMaxLength(9);
            builder.Property(p => p.Brand).IsRequired().HasMaxLength(40);
            builder.Property(p => p.Model).IsRequired().HasMaxLength(40);
            builder.Property(p => p.Fuel).IsRequired();
            builder.Property(p => p.Cv).IsRequired();
            builder.Property(p => p.NDoors).IsRequired();
            builder.Property(p => p.WheelsSize).IsRequired();
            builder.Property(p => p.Image);
            builder.Property(p => p.Activo).HasDefaultValue(true);
        }
    }
}

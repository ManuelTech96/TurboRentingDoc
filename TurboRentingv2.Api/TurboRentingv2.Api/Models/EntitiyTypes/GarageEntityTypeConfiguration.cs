using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurboRentingv2.Api.Models.Entities;

namespace TurboRentingv2.Api.Models.EntitiyTypes
{
    public class GarageEntityTypeConfiguration : IEntityTypeConfiguration<Garage>
    {
        public void Configure(EntityTypeBuilder<Garage> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(40);
            builder.Property(p => p.Address).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Location).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Phone).IsRequired().HasMaxLength(9);
            builder.Property(p => p.Capacity).IsRequired();
            builder.Property(p => p.VehiculeWasher);
            builder.Property(p => p.Activo).HasDefaultValue(true);

            builder.HasMany(g => g.Vehicules)
                   .WithOne()
                   .HasForeignKey(v => v.GarageId);
        }
    }
}

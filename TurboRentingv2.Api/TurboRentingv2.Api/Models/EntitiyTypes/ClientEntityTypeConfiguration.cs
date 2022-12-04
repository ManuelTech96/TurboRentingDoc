using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurboRentingv2.Api.Models.Entities;

namespace TurboRentingv2.Api.Models.EntitiyTypes
{
    public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Dni).IsRequired().HasMaxLength(10);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(40);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(40);
            builder.Property(p => p.Phone);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Activo).HasDefaultValue(true);

            builder.HasMany(c => c.Contracts)
                   .WithMany(c => c.Clients);

            builder.HasMany(c => c.Vehicules)
                   .WithMany(v => v.Clients);

        }
    }
}

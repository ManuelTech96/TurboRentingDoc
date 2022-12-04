using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurboRentingv2.Api.Models.Entities;

namespace TurboRentingv2.Api.Models.EntitiyTypes
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Password).IsRequired().HasMaxLength(50);
            builder.Property(p => p.RePassword).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Role).IsRequired();
            builder.Property(p => p.Activo).HasDefaultValue(true);
        }
    }
}

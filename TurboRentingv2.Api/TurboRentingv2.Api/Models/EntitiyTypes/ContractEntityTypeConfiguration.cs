using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurboRentingv2.Api.Models.Entities;

namespace TurboRentingv2.Api.Models.EntitiyTypes
{
    public class ContractEntityTypeConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ContractCode).IsRequired();
            builder.Property(p => p.TypeName).IsRequired();
            builder.Property(p => p.TypeCost).IsRequired();
            builder.Property(p => p.Duration).IsRequired();
            builder.Property(p => p.MonthlyCost).IsRequired();
            builder.Property(p => p.TotalCost);
            builder.Property(p => p.Activo).HasDefaultValue(true);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAgg;
namespace Shop.Infrastructure.EFConfigs;

internal class OrderAddressConfig : IEntityTypeConfiguration<OrderAddress>
{
    public void Configure(EntityTypeBuilder<OrderAddress> builder)
    {
        builder.ToTable("OrderAddresses");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.IranCode).IsRequired(false).HasMaxLength(10);
        builder.Property(b => b.AddressDetail).IsRequired(true).HasMaxLength(600);
        builder.Property(b => b.FullName).IsRequired(true).HasMaxLength(255);
        builder.Property(b => b.PostalCode).IsRequired(true).HasMaxLength(10);
        builder.Property(b => b.Phone).IsRequired(true).HasMaxLength(11);

    }
}

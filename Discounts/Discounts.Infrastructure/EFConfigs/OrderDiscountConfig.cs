using Discounts.Domain.OrderDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Discounts.Infrastructure.EFConfigs;
internal class OrderDiscountConfig : IEntityTypeConfiguration<OrderDiscount>
{
    public void Configure(EntityTypeBuilder<OrderDiscount> builder)
    {
        builder.ToTable("OrderDiscounts");
        builder.HasKey(x => x.Id);

        builder.Property(b => b.Type).IsRequired();
        builder.Property(b => b.Title).IsRequired().HasMaxLength(355);
        builder.Property(b => b.Code).IsRequired().HasMaxLength(10);
    }
}
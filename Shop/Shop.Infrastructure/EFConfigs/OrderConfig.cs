using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAgg;
namespace Shop.Infrastructure.EFConfigs;
internal class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.OrderStatus).IsRequired();
        builder.Property(b => b.OrderPayment).IsRequired();
        builder.Property(b => b.PostTitle).IsRequired(false).HasMaxLength(600);

        builder.HasMany(o => o.OrderSellers).WithOne(s => s.Order).HasForeignKey(s => s.OrderId);
    }
}

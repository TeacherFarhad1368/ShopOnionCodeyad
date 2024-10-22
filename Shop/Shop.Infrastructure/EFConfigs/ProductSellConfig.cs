using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ProductSellAgg;
namespace Shop.Infrastructure.EFConfigs;

internal class ProductSellConfig : IEntityTypeConfiguration<ProductSell>
{
    public void Configure(EntityTypeBuilder<ProductSell> builder)
    {
        builder.ToTable("ProductSells");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Unit).IsRequired(true).HasMaxLength(255);

        builder.HasMany(o => o.OrderItems).WithOne(s => s.ProductSell).HasForeignKey(s => s.ProductSellId);
        builder.HasOne(o => o.Product).WithMany(s => s.ProductSells).HasForeignKey(s => s.ProductId);
        builder.HasOne(o => o.Seller).WithMany(s => s.ProductSells).HasForeignKey(s => s.SellerId);
    }
}

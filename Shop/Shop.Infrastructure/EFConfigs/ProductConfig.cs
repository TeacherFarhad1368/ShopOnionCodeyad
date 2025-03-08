using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ProductAgg;
namespace Shop.Infrastructure.EFConfigs;

internal class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title).IsRequired(true).HasMaxLength(255);
        builder.Property(b => b.Slug).IsRequired(true).HasMaxLength(355);
        builder.Property(b => b.ImageName).IsRequired(true).HasMaxLength(155);
        builder.Property(b => b.ImageAlt).IsRequired(true).HasMaxLength(155);
        builder.Property(b => b.Description).IsRequired(true);
        builder.Property(b => b.ShortDescription).IsRequired(true);

        builder.HasMany(o => o.ProductGalleries).WithOne(s => s.Product).HasForeignKey(s => s.ProductId);
        builder.HasMany(o => o.ProductFeatures).WithOne(s => s.Product).HasForeignKey(s => s.ProductId);
        builder.HasMany(o => o.ProductCategoryRelations).WithOne(s => s.Product).HasForeignKey(s => s.ProductId);
        builder.HasMany(o => o.ProductSells).WithOne(s => s.Product).HasForeignKey(s => s.ProductId);
        builder.HasMany(o => o.ProductVisits).WithOne(s => s.Product).HasForeignKey(s => s.ProductId);
        builder.HasMany(o => o.WishLists).WithOne(s => s.Product).HasForeignKey(s => s.ProductId);
    }
}

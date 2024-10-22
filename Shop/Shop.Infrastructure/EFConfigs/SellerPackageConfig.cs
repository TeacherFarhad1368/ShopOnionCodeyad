using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SellerPackageAgg;
namespace Shop.Infrastructure.EFConfigs;

internal class SellerPackageConfig : IEntityTypeConfiguration<SellerPackage>
{
    public void Configure(EntityTypeBuilder<SellerPackage> builder)
    {
        builder.ToTable("SellerPackages");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title).IsRequired(true).HasMaxLength(255);
        builder.Property(b => b.Description).IsRequired(true);

        builder.HasMany(o => o.SellerPackageFeatures).WithOne(s => s.SellerPackage).HasForeignKey(s => s.SellerPackageId);
    }
}

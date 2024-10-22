using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SellerPackageFeatureAgg;
namespace Shop.Infrastructure.EFConfigs;

internal class SellerPackageFeatureConfig : IEntityTypeConfiguration<SellerPackageFeature>
{
    public void Configure(EntityTypeBuilder<SellerPackageFeature> builder)
    {
        builder.ToTable("SellerPackageFeatures");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title).IsRequired(true).HasMaxLength(255);
        builder.Property(b => b.Description).IsRequired(true);

        builder.HasOne(o => o.SellerPackage).WithMany(s => s.SellerPackageFeatures).HasForeignKey(s => s.SellerPackageId);
    }
}

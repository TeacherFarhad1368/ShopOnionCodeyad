using Discounts.Domain.ProductDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Discounts.Infrastructure.EFConfigs;
internal class ProductDiscountConfig : IEntityTypeConfiguration<ProductDiscount>
{
    public void Configure(EntityTypeBuilder<ProductDiscount> builder)
    {
        builder.ToTable("ProductDiscounts");
        builder.HasKey(x => x.Id);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stores.Domain.StoreProductAgg;

namespace Stores.Infrastructure.EFConfigs;

internal class StoreProductConfig : IEntityTypeConfiguration<StoreProduct>
{
    public void Configure(EntityTypeBuilder<StoreProduct> builder)
    {
        builder.ToTable("StoreProducts");
        builder.HasKey(x => x.Id);
        builder.Property(b => b.Type).IsRequired();

        builder.HasOne(b => b.Store).WithMany(s => s.StoreProducts).HasForeignKey(s => s.StoreId);
    }
}
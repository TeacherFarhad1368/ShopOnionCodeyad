using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stores.Domain.StoreAgg;

namespace Stores.Infrastructure.EFConfigs;
internal class StoreConfig : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Stores");
        builder.HasKey(x => x.Id);
        builder.Property(b=>b.Description).IsRequired();

        builder.HasMany(b => b.StoreProducts).WithOne(s => s.Store).HasForeignKey(s => s.StoreId);
    }
}

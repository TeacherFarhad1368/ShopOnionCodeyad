using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ShopAgg;
namespace Shop.Infrastructure.EFConfigs;

internal class ShopSettingConfig : IEntityTypeConfiguration<ShopSetting>
{
    public void Configure(EntityTypeBuilder<ShopSetting> builder)
    {
        builder.ToTable("ShopSettings");
        builder.HasKey(b => b.Id);

    }
}
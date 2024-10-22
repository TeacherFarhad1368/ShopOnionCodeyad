using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SellerAgg;
namespace Shop.Infrastructure.EFConfigs;

internal class SellerConfig : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.ToTable("Sellers");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title).IsRequired(true).HasMaxLength(255);
        builder.Property(b => b.Address).IsRequired(true).HasMaxLength(600);
        builder.Property(b => b.ImageAccept).IsRequired(true).HasMaxLength(155);
        builder.Property(b => b.ImageName).IsRequired(true).HasMaxLength(155);
        builder.Property(b => b.ImageAlt).IsRequired(true).HasMaxLength(155);
        builder.Property(b => b.WhatsApp).IsRequired(false).HasMaxLength(800);
        builder.Property(b => b.Telegram).IsRequired(false).HasMaxLength(800);
        builder.Property(b => b.Instagram).IsRequired(false).HasMaxLength(800);
        builder.Property(b => b.Phone1).IsRequired(true).HasMaxLength(11);
        builder.Property(b => b.Phone2).IsRequired(false).HasMaxLength(11);
        builder.Property(b => b.Email).IsRequired(false).HasMaxLength(255);
        builder.Property(b => b.GoogleMapUrl).IsRequired(false);
        builder.Property(b => b.Status).IsRequired(true);

        builder.HasMany(o => o.ProductSells).WithOne(s => s.Seller).HasForeignKey(s => s.SellerId);
    }
}

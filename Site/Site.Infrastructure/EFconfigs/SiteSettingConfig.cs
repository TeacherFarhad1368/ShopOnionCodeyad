using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.SiteSettingAgg;

namespace Site.Infrastructure.EFconfigs;

internal class SiteSettingConfig : IEntityTypeConfiguration<SiteSetting>
{
    public void Configure(EntityTypeBuilder<SiteSetting> builder)
    {
        builder.ToTable("SiteSettings");
        builder.HasKey(x => x.Id);
        builder.Property(b => b.Android).IsRequired(false).HasMaxLength(800);
        builder.Property(b => b.FooterTitle).IsRequired(false).HasMaxLength(255);
        builder.Property(b => b.FooterDescription).IsRequired(false);
        builder.Property(b => b.AboutTitle).IsRequired(false).HasMaxLength(255);
        builder.Property(b => b.AboutDescription).IsRequired(false);
        builder.Property(b => b.Address).IsRequired(false).HasMaxLength(400);
        builder.Property(b => b.ContactDescription).IsRequired(false);
        builder.Property(b => b.Email1).IsRequired(false).HasMaxLength(255);
        builder.Property(b => b.Email2).IsRequired(false).HasMaxLength(255);
        builder.Property(b => b.WhatsApp).IsRequired(false).HasMaxLength(600);
        builder.Property(b => b.SeoBox).IsRequired(false);
        builder.Property(b => b.Phone1).IsRequired(false).HasMaxLength(11);
        builder.Property(b => b.Phone2).IsRequired(false).HasMaxLength(11);
        builder.Property(b => b.Enamad).IsRequired(false);
        builder.Property(b => b.FavIcon).IsRequired(false).HasMaxLength(155);
        builder.Property(b => b.Instagram).IsRequired(false).HasMaxLength(600);
        builder.Property(b => b.IOS).IsRequired(false).HasMaxLength(800);
        builder.Property(b => b.Youtube).IsRequired(false).HasMaxLength(600);
        builder.Property(b => b.Telegram).IsRequired(false).HasMaxLength(600);
        builder.Property(b => b.SamanDehi).IsRequired(false);
        builder.Property(b => b.LogoAlt).IsRequired(false).HasMaxLength(155);
        builder.Property(b => b.LogoName).IsRequired(false).HasMaxLength(155);
    }
}
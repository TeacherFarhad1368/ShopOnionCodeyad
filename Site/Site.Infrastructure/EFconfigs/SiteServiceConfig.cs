using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.SiteServiceAgg;

namespace Site.Infrastructure.EFconfigs;

internal class SiteServiceConfig : IEntityTypeConfiguration<SiteService>
{
    public void Configure(EntityTypeBuilder<SiteService> builder)
    {
        builder.ToTable("SiteServices");
        builder.HasKey(x => x.Id);
        builder.Property(b => b.Title).IsRequired(true).HasMaxLength(400);
        builder.Property(b => b.ImageName).IsRequired(true).HasMaxLength(155);
        builder.Property(b => b.ImageAlt).IsRequired(true).HasMaxLength(155);
    }
}

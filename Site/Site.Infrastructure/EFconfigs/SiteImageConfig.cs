using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.SiteImageAgg;

namespace Site.Infrastructure.EFconfigs;

internal class SiteImageConfig : IEntityTypeConfiguration<SiteImage>
{
	public void Configure(EntityTypeBuilder<SiteImage> builder)
	{
		builder.ToTable("Images");
		builder.HasKey(x => x.Id);
		builder.Property(b => b.Title).IsRequired(true).HasMaxLength(255);
		builder.Property(b => b.ImageName).IsRequired(true).HasMaxLength(155);
	}
}
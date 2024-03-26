using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.SliderAgg;

namespace Site.Infrastructure.EFconfigs;

internal class SliderConfig : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        builder.ToTable("Sliders");
        builder.HasKey(x => x.Id);
        builder.Property(b => b.ImageName).IsRequired(true).HasMaxLength(155);
        builder.Property(b => b.ImageAlt).IsRequired(true).HasMaxLength(155);
        builder.Property(b => b.Url).IsRequired(true).HasMaxLength(900);
    }
}

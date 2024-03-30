using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.MenuAgg;

namespace Site.Infrastructure.EFconfigs;

internal class MenuConfig : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");
        builder.HasKey(x => x.Id);
        builder.Property(b => b.Status).IsRequired(true);
        builder.Property(b => b.Url).IsRequired(true).HasMaxLength(600);
        builder.Property(b => b.ImageName).IsRequired(false).HasMaxLength(155);
        builder.Property(b => b.ImageAlt).IsRequired(false).HasMaxLength(155);
        builder.Property(b => b.Title).IsRequired(true).HasMaxLength(255);

        builder.HasOne(b => b.Parent).WithMany(m => m.Childs).HasForeignKey(b => b.ParentId);
    }
}

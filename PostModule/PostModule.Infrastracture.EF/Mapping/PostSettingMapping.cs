using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostModule.Domain.SettingAgg;

namespace PostModule.Infrastracture.EF.Mapping
{
    public class PostSettingMapping : IEntityTypeConfiguration<PostSetting>
    {
        public void Configure(EntityTypeBuilder<PostSetting> builder)
        {
            builder.ToTable("PostSettings");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.PackageTitle).IsRequired(false).HasMaxLength(255);
            builder.Property(b => b.PackageDescription).IsRequired(false);
            builder.Property(b => b.ApiDescription).IsRequired(false);

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostModule.Domain.UserPostAgg;

namespace PostModule.Infrastracture.EF.Mapping;

public class UserPostMapping : IEntityTypeConfiguration<UserPost>
{
    public void Configure(EntityTypeBuilder<UserPost> builder)
    {
        builder.ToTable("UserPosts");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.ApiCode).IsRequired(true).HasMaxLength(50);
    }
}
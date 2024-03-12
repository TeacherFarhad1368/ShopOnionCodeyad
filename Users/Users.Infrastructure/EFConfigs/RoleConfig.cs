using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.UserAgg;

namespace Users.Infrastructure.EFConfigs
{
    internal class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title).IsRequired().HasMaxLength(255);

            builder.HasMany(b => b.Permissions)
                .WithOne(a => a.Role).HasForeignKey(a => a.RoleId);

            builder.HasMany(b => b.UserRoles)
                .WithOne(a => a.Role).HasForeignKey(a => a.RoleId);
        }
    }
}

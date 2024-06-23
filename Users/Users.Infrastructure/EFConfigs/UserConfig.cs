using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.UserAgg;

namespace Users.Infrastructure.EFConfigs;

internal class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(b => b.Id);

        builder.Property(b=>b.FullName).IsRequired(false).HasMaxLength(255);
        builder.Property(b=>b.Avatar).IsRequired().HasMaxLength(200);
        builder.Property(b=>b.Email).IsRequired(false).HasMaxLength(255);
        builder.Property(b=>b.Mobile).IsRequired().HasMaxLength(11);
        builder.Property(b=>b.UserGender).IsRequired();
        builder.Property(b => b.Password).IsRequired().HasMaxLength(100);

        builder.HasMany(b => b.UserAddresses)
            .WithOne(a => a.User).HasForeignKey(a => a.UserId);

        builder.HasMany(b => b.UserRoles)
            .WithOne(a => a.User).HasForeignKey(a => a.UserId);

        builder.HasMany(b => b.Wallets)
          .WithOne(a => a.User).HasForeignKey(a => a.UserId);
    }
}

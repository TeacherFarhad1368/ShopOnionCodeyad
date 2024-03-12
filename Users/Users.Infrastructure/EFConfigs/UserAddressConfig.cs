using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.UserAgg;

namespace Users.Infrastructure.EFConfigs
{
    internal class UserAddressConfig : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("UserAddresses");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.FullName).IsRequired().HasMaxLength(255);
            builder.Property(b => b.AddressDetail).IsRequired().HasMaxLength(500);
            builder.Property(b => b.IranCode).IsRequired().HasMaxLength(10);
            builder.Property(b => b.Phone).IsRequired().HasMaxLength(11);
            builder.Property(b => b.PostalCode).IsRequired().HasMaxLength(10);

            builder.HasOne(b => b.User)
                .WithMany(a => a.UserAddresses).HasForeignKey(a => a.UserId);
        }
    }
}

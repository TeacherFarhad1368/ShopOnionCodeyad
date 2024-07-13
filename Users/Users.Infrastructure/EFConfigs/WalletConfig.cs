using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.WalletAgg;

namespace Users.Infrastructure.EFConfigs;

internal class WalletConfig : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Description).IsRequired(false).HasMaxLength(455);
        builder.Property(b => b.Type).IsRequired();
        builder.Property(b => b.WalletWhy).IsRequired();
        
        builder.HasOne(b => b.User)
          .WithMany(a => a.Wallets).HasForeignKey(a => a.UserId);
    }
}
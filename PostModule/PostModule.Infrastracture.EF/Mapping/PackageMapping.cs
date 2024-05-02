using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostModule.Domain.UserPostAgg;

namespace PostModule.Infrastracture.EF.Mapping;

public class    PackageMapping : IEntityTypeConfiguration<Package>
{
    public void Configure(EntityTypeBuilder<Package> builder)
    {
        builder.ToTable("Packages");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title).IsRequired(true).HasMaxLength(255);
        builder.Property(b => b.Description).IsRequired(true);

        builder.HasMany(p=>p.PostOrders).WithOne(o=>o.Package).HasForeignKey(p => p.PackageId);
    }
}

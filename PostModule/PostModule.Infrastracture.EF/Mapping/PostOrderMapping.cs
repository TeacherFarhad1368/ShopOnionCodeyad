using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostModule.Domain.UserPostAgg;

namespace PostModule.Infrastracture.EF.Mapping;

public class PostOrderMapping : IEntityTypeConfiguration<PostOrder>
{
    public void Configure(EntityTypeBuilder<PostOrder> builder)
    {
        builder.ToTable("PostOrders");
        builder.HasKey(b => b.Id);

        builder.HasOne(b => b.Package).WithMany(b => b.PostOrders).HasForeignKey(p => p.PackageId);
    }
}

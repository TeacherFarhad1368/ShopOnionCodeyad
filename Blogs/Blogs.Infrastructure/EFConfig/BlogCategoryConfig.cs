using Blogs.Domain.BlogCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Infrastructure.EFConfig
{
    internal class BlogCategoryConfig : IEntityTypeConfiguration<BlogCategory>
    {
        public void Configure(EntityTypeBuilder<BlogCategory> builder)
        {
            builder.ToTable("BlogCategories");
            builder.HasKey(x => x.Id);

            builder.Property(b => b.Title).IsRequired().HasMaxLength(250);
            builder.Property(b => b.Slug).IsRequired().HasMaxLength(300);
            builder.Property(b => b.ImageName).IsRequired().HasMaxLength(150);
            builder.Property(b => b.ImageAlt).IsRequired().HasMaxLength(150);
        }
    }
}

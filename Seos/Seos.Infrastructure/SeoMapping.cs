using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seos.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Seos.Infrastructure
{
    public class SeoMapping : IEntityTypeConfiguration<Seo>
    {
        public void Configure(EntityTypeBuilder<Domain.Seo> builder)
        {
            builder.ToTable("Seos");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.MetaTitle).IsRequired(true).HasMaxLength(500);
            builder.Property(b => b.MetaDescription).IsRequired(false).HasMaxLength(800);
            builder.Property(b => b.MetaKeyWords).IsRequired(false).HasMaxLength(500);
            builder.Property(b => b.Where).IsRequired(true);
            builder.Property(b => b.Canonical).IsRequired(false).HasMaxLength(500);
            builder.Property(b => b.Schema).IsRequired(false).HasMaxLength(500);
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAgg;
namespace Shop.Infrastructure.EFConfigs;

internal class OrderSellerConfig : IEntityTypeConfiguration<OrderSeller>
{
    public void Configure(EntityTypeBuilder<OrderSeller> builder)
    {
        builder.ToTable("OrderSellers");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Status).IsRequired();

        builder.HasMany(o => o.OrderItems).WithOne(s => s.OrderSeller).HasForeignKey(s => s.OrderSellerId);
        builder.HasOne(o => o.Order).WithMany(s => s.OrderSellers).HasForeignKey(s => s.OrderId);
    }
}
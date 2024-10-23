using Discounts.Domain.OrderDiscountAgg;
using Discounts.Domain.ProductDiscountAgg;
using Discounts.Infrastructure.EFConfigs;
using Microsoft.EntityFrameworkCore;
namespace Discounts.Infrastructure;
public class DiscountContext : DbContext
{
    public DiscountContext(DbContextOptions<DiscountContext> options) : base(options) { }
    public DbSet<OrderDiscount> OrderDiscounts { get; set; }
    public DbSet<ProductDiscount> ProductDiscounts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDiscountConfig).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

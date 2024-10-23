using Microsoft.EntityFrameworkCore;
using Stores.Domain.StoreAgg;
using Stores.Domain.StoreProductAgg;
using Stores.Infrastructure.EFConfigs;

namespace Stores.Infrastructure
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options): base(options) { }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreProduct> StoreProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreConfig).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductCategoryAgg;
using Shop.Domain.ProductCategoryRelationAgg;
using Shop.Domain.ProductGalleryAgg;
using Shop.Domain.ProductSellAgg;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerPackageAgg;
using Shop.Domain.SellerPackageFeatureAgg;
using Shop.Domain.ShopAgg;
using Shop.Infrastructure.EFConfigs;

namespace Shop.Infrastructure
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options):base(options)
        {
            
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderAddress> OrderAddresses { get; set; }
        public DbSet<OrderSeller> OrderSellers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCategoryRelation> ProductCategoryRelations { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }
        public DbSet<ProductSell> ProductSells { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SellerPackage> SellerPackages { get; set; }
        public DbSet<SellerPackageFeature> SellerPackageFeatures { get; set; }
        public DbSet<ShopSetting> ShopSettings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfig).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

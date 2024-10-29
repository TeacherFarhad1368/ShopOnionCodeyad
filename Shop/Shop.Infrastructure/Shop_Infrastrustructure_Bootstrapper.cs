using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductCategoryAgg;
using Shop.Domain.ProductCategoryRelationAgg;
using Shop.Domain.ProductGalleryAgg;
using Shop.Domain.ProductSellAgg;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerPackageAgg;
using Shop.Domain.SellerPackageFeatureAgg;
using Shop.Infrastructure.Services;

namespace Shop.Infrastructure
{
    public static class Shop_Infrastrustructure_Bootstrapper
    {
        public static void Config_Shop_Infrastructure(this IServiceCollection services,string connection)
        {
            services.Config_Shop_Application();

            services.AddTransient<IOrderRepository,OrderRepository>();  
            services.AddTransient<IProductCategoryRelationRepository, ProductCategoryRelationRepository>();  
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();  
            services.AddTransient<IProductFeatureRepository, ProductFeatureRepository>();  
            services.AddTransient<IProductGalleryRepository, ProductGalleryRepository>();  
            services.AddTransient<IProductRepository, ProductRepository>();  
            services.AddTransient<IProductSellRepository, ProductSellRepository>();  
            services.AddTransient<ISellerPackageRepository, SellerPackageRepository>();  
            services.AddTransient<ISellerPackageFeatureRepository, SellerPackageFeatureRepository>();  
            services.AddTransient<ISellerRepository, SellerRepository>();

            services.AddDbContext<ShopContext>(x =>
            {
                x.UseSqlServer(connection);
            });
        }
    }
}

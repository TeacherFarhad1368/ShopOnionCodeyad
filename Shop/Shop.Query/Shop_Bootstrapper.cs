using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Contract.OrderApplication.Query;
using Shop.Application.Contract.ProductApplication;
using Shop.Application.Contract.ProductCategoryApplication.Query;
using Shop.Application.Contract.ProductFeautreApplication.Query;
using Shop.Application.Contract.ProductGalleryApplication.Query;
using Shop.Application.Contract.ProductSellApplication.Query;
using Shop.Application.Contract.SellerApplication.Query;
using Shop.Application.Contract.SellerPackegaApplication.Query;
using Shop.Infrastructure;
using Shop.Query.Services;

namespace Shop.Query
{
    public static class Shop_Bootstrapper
    {
        public static void ConfigShopModule(this IServiceCollection services ,string connection)
        {
            services.Config_Shop_Infrastructure(connection);

            services.AddTransient<IOrderQuery, OrderQuery>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddTransient<IProductFeautreQuery, ProductFeatureQuery>();
            services.AddTransient<IProductGalleryQuery, ProductGalleryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();
            services.AddTransient<IProductSellQuery, ProductSellQuery>();
            services.AddTransient<ISellerQuery, SellerQuery>();
            services.AddTransient<ISellerPackegaQuery, SellerPackegaQuery>();
        }
    }
}

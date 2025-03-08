using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Contract.OrderApplication.Command;
using Shop.Application.Contract.ProductApplication.Command;
using Shop.Application.Contract.ProductCategoryApplication.Command;
using Shop.Application.Contract.ProductFeautreApplication.Command;
using Shop.Application.Contract.ProductGalleryApplication.Command;
using Shop.Application.Contract.ProductSellApplication.Command;
using Shop.Application.Contract.ProductVisitApplication.Command;
using Shop.Application.Contract.SellerApplication.Command;
using Shop.Application.Contract.SellerPackegaApplication.Command;
using Shop.Application.Contract.WishListApplication.Command;
using Shop.Application.Services;
namespace Shop.Application;

public static class ShopApplicationBootstrapper
{
    public static void Config_Shop_Application(this IServiceCollection services)
    {
        services.AddTransient<IProductApplication,ProductApplication>();    
        services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();    
        services.AddTransient<IProductFeautreApplication, ProductFeautreApplication>();    
        services.AddTransient<IProductGalleryApplication, ProductGalleryApplication>();
        services.AddTransient<ISellerApplication,SellerApplication>();
        services.AddTransient<IProductSellApplication, ProductSellApplication>();
        services.AddTransient<ISellerPackegaApplication, SellerPackegaApplication>();
        services.AddTransient<IOrderApplication, OrderApplication>();
        services.AddTransient<IProductVisitApplication, ProductVisitApplication>();
        services.AddTransient<IWishListApplication, WishListApplication>();
    }
}
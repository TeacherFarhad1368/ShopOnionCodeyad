using Discounts.Application.Contract.OrderDiscountApplication.Command;
using Discounts.Application.Contract.ProductDiscountApplication.Command;
using Discounts.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Application
{
    public static class DiscountApplicationBootstrapper
    {
        public static void Config_Discount_Application(this IServiceCollection services)
        {
            services.AddTransient<IOrderDiscountApplication, OrderDiscountApplication>();
            services.AddTransient<IProductDiscountApplication, ProductDiscountApplication>();
        }
    }
}

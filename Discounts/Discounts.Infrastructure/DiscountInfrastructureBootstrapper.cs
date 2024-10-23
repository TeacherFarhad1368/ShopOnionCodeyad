using Discount.Application;
using Discounts.Domain.OrderDiscountAgg;
using Discounts.Domain.ProductDiscountAgg;
using Discounts.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Discounts.Infrastructure
{
    public static class DiscountInfrastructureBootstrapper
    {
        public static void Config_Discount_Infrastructure(this IServiceCollection services,string connection)
        {
            services.Config_Discount_Application();

            services.AddTransient<IOrderDiscountRepository, OrderDiscountRepository>();
            services.AddTransient<IProductDiscountRepository, ProductDiscountRepository>();

            services.AddDbContext<DiscountContext>(x =>
            {
                x.UseSqlServer(connection);
            });
        }
    }
}

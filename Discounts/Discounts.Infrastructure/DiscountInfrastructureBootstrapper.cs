using Discount.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Discounts.Infrastructure
{
    public static class DiscountInfrastructureBootstrapper
    {
        public static void Config_Discount_Infrastructure(this IServiceCollection services,string connection)
        {
            services.Config_Discount_Application();

            services.AddDbContext<DiscountContext>(x =>
            {
                x.UseSqlServer(connection);
            });
        }
    }
}

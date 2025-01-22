
using Discounts.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Query
{
    public static class Discount_Bootstrapper
    {
        public static void Config_Discount_Module(this IServiceCollection services,string connection)
        {
            services.Config_Discount_Infrastructure(connection);
        }
    }
}

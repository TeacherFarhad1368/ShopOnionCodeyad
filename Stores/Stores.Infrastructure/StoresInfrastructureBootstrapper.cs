using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stores.Application;

namespace Stores.Infrastructure
{
    public static class StoresInfrastructureBootstrapper
    {
        public static void Config_Stores_Infrastructure(this IServiceCollection services,string connection)
        {
            services.Config_Store_Application();

            services.AddDbContext<StoreContext>(x =>
            {
                x.UseSqlServer(connection);
            });
        }
    }
}

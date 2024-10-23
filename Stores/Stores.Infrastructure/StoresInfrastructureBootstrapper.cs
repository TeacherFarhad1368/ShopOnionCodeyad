using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stores.Application;
using Stores.Domain.StoreAgg;
using Stores.Domain.StoreProductAgg;
using Stores.Infrastructure.Services;

namespace Stores.Infrastructure
{
    public static class StoresInfrastructureBootstrapper
    {
        public static void Config_Stores_Infrastructure(this IServiceCollection services,string connection)
        {
            services.Config_Store_Application();

            services.AddTransient<IStoreRepository,StoreRepository>();  
            services.AddTransient<IStoreProductRepository,StoreProductRepository>();

            services.AddDbContext<StoreContext>(x =>
            {
                x.UseSqlServer(connection);
            });
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Stores.Application.Contract.StoreApplication.Command;
using Stores.Application.Services;

namespace Stores.Application
{
    public static class StoresApplicationBootstrapper
    {
        public static void Config_Store_Application(this IServiceCollection services)
        {
            services.AddTransient<IStoreApplication, StoreApplication>();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Stores.Infrastructure;
namespace Stores.Query
{
    public static class Stores_Bootstrapper
    {
        public static void Config_Store_Module(this IServiceCollection services,string connection)
        {
            services.Config_Stores_Infrastructure(connection);
        }
    }
}

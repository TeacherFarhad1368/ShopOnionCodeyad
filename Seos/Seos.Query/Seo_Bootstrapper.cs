using Microsoft.Extensions.DependencyInjection;
using Seos.Application.Contract;
using Seos.Infrastructure;

namespace Seos.Query
{
    public static class Seo_Bootstrapper
    {
        public static void Config(IServiceCollection services, string connection)
        {
            services.AddTransient<ISeoQuery, SeoQuery>();
            SeoInfrastructureBootstrapper.Config(services, connection);
        }
    }
}
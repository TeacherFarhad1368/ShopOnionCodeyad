using Microsoft.Extensions.DependencyInjection;
using Seos.Infrastructure;

namespace Seos.Query
{
    public static class Seo_Bootstrapper
    {
        public static void Config(IServiceCollection services, string connection)
        {
            SeoInfrastructureBootstrapper.Config(services, connection);
        }
    }
}
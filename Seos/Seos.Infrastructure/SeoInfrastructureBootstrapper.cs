using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Seos.Application;
using Seos.Domain;

namespace Seos.Infrastructure
{
    public static class SeoInfrastructureBootstrapper
    {
        public static void Config(IServiceCollection services, string connection)
        {
            SeoApplicationBootstrapper.Config(services);

            services.AddTransient<ISeoRepository, SeoRepository>();

            services.AddDbContext<Seo_Context>(x =>
            {
                x.UseSqlServer(connection);
            });
        }
    }
}
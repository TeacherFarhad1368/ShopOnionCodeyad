using Microsoft.Extensions.DependencyInjection;
using Seos.Application.Contract;
using Seos.Application.Services;

namespace Seos.Application
{
    public static class SeoApplicationBootstrapper
    {
        public static void Config(IServiceCollection services)
        {
            services.AddTransient<ISeoApplication, SeoApplication>();
        }
    }
}
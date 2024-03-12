using Microsoft.Extensions.DependencyInjection;
using Site.Application.Contract.BanerApplication.Query;
using Site.Application.Contract.MenuApplication.Query;
using Site.Application.Contract.SiteServiceApplication.Query;
using Site.Application.Contract.SiteSettingApplication.Query;
using Site.Application.Contract.SliderApplication.Query;
using Site.Infrastructure;
using Site.Query.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Query
{
    public static class Site_Bootstrapper
    {
        public static void Config(IServiceCollection services, string connectionString)
        {
            SiteInfrastructureBootstrapper.Config(services, connectionString);

            services.AddTransient<IBanerQuery,BanerQuery>();    
            services.AddTransient<IMenuQuery,MenuQuery>();    
            services.AddTransient<ISliderQuery,SliderQuery>();    
            services.AddTransient<ISiteSettingQuery, SiteSettingQuery>();    
            services.AddTransient<ISiteServiceQuery,SiteServiceQuery>();
        }
    }
}

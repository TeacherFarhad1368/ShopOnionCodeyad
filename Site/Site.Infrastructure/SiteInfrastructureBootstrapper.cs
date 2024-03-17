using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Site.Application;
using Site.Domain.BanerAgg;
using Site.Domain.MenuAgg;
using Site.Domain.SiteImageAgg;
using Site.Domain.SitePageAgg;
using Site.Domain.SiteServiceAgg;
using Site.Domain.SiteSettingAgg;
using Site.Domain.SliderAgg;
using Site.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure
{
    public static class SiteInfrastructureBootstrapper
    {
        public static void Config(IServiceCollection services,string connectionString)
        {
            services.AddDbContext<SiteContext>(x =>
            {
                x.UseSqlServer(connectionString);
            });

            services.AddTransient<ISiteSettingRepository, SiteSettingRepository>();
            services.AddTransient<ISiteServiceRepository, SiteServiceRepository>();
            services.AddTransient<IBanerRepository, BanerRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<ISliderRepository,SliderRepository>();
            services.AddTransient<IImageSiteRepository,ImageSiteRepository>();
            services.AddTransient<ISitePageRepository, SitePageRepository>();

            SiteApplicationBootstraper.Config(services);    
        }
    }
}

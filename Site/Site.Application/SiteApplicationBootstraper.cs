using Microsoft.Extensions.DependencyInjection;
using Site.Application.Contract.BanerApplication.Command;
using Site.Application.Contract.ImageSiteApplication.Command;
using Site.Application.Contract.MenuApplication.Command;
using Site.Application.Contract.SitePageApplication.Command;
using Site.Application.Contract.SiteServiceApplication.Command;
using Site.Application.Contract.SiteSettingApplication.Command;
using Site.Application.Contract.SliderApplication.Command;
using Site.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application
{
    public static class SiteApplicationBootstraper
    {
        public static void Config(IServiceCollection services)
        {
            services.AddTransient<IBanerApplication, BanerApplication>();
            services.AddTransient<IMenuApplication, MenuApplication>();
            services.AddTransient<ISiteServiceApplication, SiteServiceApplication>();
            services.AddTransient<ISiteSettingApplication, SiteSettingApplication>();
            services.AddTransient<ISliderApplication, SliderApplication>();
            services.AddTransient<ISitePageApplication, SitePageApplication>();
            services.AddTransient<IImageSiteApplication, ImageSiteApplication>();
        }
    }
}

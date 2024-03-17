using Microsoft.Extensions.DependencyInjection;
using PostModule.Application.Contract.CityApplication;
using PostModule.Application.Contract.PostApplication;
using PostModule.Application.Contract.PostPriceApplication;
using PostModule.Application.Contract.StateApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Services
{
    public class PostApplicationBootstrapper
    {
        public static void Config(IServiceCollection services)
        {
            services.AddTransient<IStateApplication, StateApplication>();
            services.AddTransient<ICityApplication, CityApplication>();
            services.AddTransient<IPostApplication, PostApplication>();
            services.AddTransient<IPostPriceApplication, PostPriceApplication>();

        }
    }
}

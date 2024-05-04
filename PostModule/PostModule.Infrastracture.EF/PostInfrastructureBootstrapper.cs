using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PostModule.Domain.Services;
using PostModule.Domain.SettingAgg;
using PostModule.Domain.UserPostAgg;
using PostModule.Infrastracture.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Infrastracture.EF
{
    public class PostInfrastructureBootstrapper
    {
        public static void Config(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostPriceRepository, PostPriceRepository>();
            services.AddTransient<IPackageRepository, PackageRepository>();
            services.AddTransient<IUserPostRepository, UserPostRepository>();
            services.AddTransient<IPOstOrderRepository, PostOrderRepository>();
            services.AddTransient<IPostSettingRepository, PostSettingRepository>();

            services.AddDbContext<Post_Context>(x =>
            {
                x.UseSqlServer(connectionString);
            });
        }
    }
}

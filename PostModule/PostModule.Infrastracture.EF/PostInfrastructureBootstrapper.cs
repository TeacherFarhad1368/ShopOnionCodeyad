using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PostModule.Domain.Services;
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

            services.AddDbContext<Post_Context>(x =>
            {
                x.UseSqlServer(connectionString);
            });
        }
    }
}

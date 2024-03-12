using Blogs.Application;
using Blogs.Domain.BlogAgg;
using Blogs.Domain.BlogCategoryAgg;
using Blogs.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Infrastructure
{
    public class BlogInfrastructureBootstrapper
    {
        public static void Config(IServiceCollection services , string connection)
        {
            BlogApplicationBootstrapper.Config(services);

            services.AddTransient<IBlogCategoryRepository, BlogCategoryRepository>();
            services.AddTransient<IBlogRepository, BlogRepository>();

            services.AddDbContext<BlogContext>(x =>
            {
                x.UseSqlServer(connection);
            });
        }
    }
}

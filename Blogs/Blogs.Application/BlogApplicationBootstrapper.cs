using Blogs.Application.Contract.BlogApplication.Command;
using Blogs.Application.Contract.BlogCategoryApplication.Command;
using Blogs.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Blogs.Application
{
    public class BlogApplicationBootstrapper
    {
        public static void Config(IServiceCollection services)
        {
            services.AddTransient<IBlogApplication, BlogApplication>();
            services.AddTransient<IBlogCategoryApplication, BlogCategoryApplication>();
        }
    }
}
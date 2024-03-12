using Blogs.Application;
using Blogs.Application.Contract.BlogApplication.Query;
using Blogs.Application.Contract.BlogCategoryApplication.Query;
using Blogs.Infrastructure;
using Blogs.Query.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Blogs.Query
{
    public class Blog_Bootstrapper
    {
        public static void Config(IServiceCollection services , string connection)
        {
           BlogInfrastructureBootstrapper.Config(services, connection);

            services.AddTransient<IBlogCategoryQuery, BlogCategoryQuery>();
            services.AddTransient<IBlogQuery, BlogQuery>();
        }
    }
}
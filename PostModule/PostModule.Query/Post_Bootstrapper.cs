using Microsoft.Extensions.DependencyInjection;
using PostModule.Application.Contract.PostQuery;
using PostModule.Application.Contract.StateQuery;
using PostModule.Application.Services;
using PostModule.Domain.Services;
using PostModule.Infrastracture.EF;
using PostModule.Infrastracture.EF.Repositories;
using PostModule.Query.Services;

namespace PostModule.Query
{
    public class Post_Bootstrapper
    {
        public static void Config(IServiceCollection services , string connectionString)
        {
            PostInfrastructureBootstrapper.Config(services, connectionString);
            PostApplicationBootstrapper.Config(services);

            services.AddTransient<IStateQuery, StateQuery>();
            services.AddTransient<IPostQuery, PostQuery>();
        }
    }
}
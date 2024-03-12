using Microsoft.Extensions.DependencyInjection;
using Users.Application.Contract.RoleApplication.Query;
using Users.Infrastructure;
using Users.Query.Services;

namespace Users.Query
{
    public class User_Bootstrapper
    {
        public static void Config(IServiceCollection services, string connection)
        {
            UserInfrastructureBootstrapper.Config(services, connection);

            services.AddTransient<IRoleQuery, RoleQuery>();
        }
    }
}
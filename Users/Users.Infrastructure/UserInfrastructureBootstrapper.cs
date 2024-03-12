using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application;
using Users.Domain.UserAgg.Repository;
using Users.Infrastructure.Service;

namespace Users.Infrastructure
{
    public class UserInfrastructureBootstrapper
    {
        public static void Config(IServiceCollection services, string connection)
        {
            UserApplicationBootstrapper.Config(services);

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserAdressRepository, UserAddressRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddDbContext<UserContext>(x =>
            {
                x.UseSqlServer(connection);
            });
        }
    }
}

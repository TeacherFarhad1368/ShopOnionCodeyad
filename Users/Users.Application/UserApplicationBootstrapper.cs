using Microsoft.Extensions.DependencyInjection;
using Users.Application.Contract.RoleApplication.Command;
using Users.Application.Contract.UserAddressApplication.Command;
using Users.Application.Contract.UserApplication.Command;
using Users.Application.Contract.WalletApplication.Command;
using Users.Application.Services;

namespace Users.Application
{
    public class UserApplicationBootstrapper
    {
        public static void Config(IServiceCollection services)
        {
            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IUserAddressApplication, UserAddressApplication>();
            services.AddTransient<IUserApplication, UserApplication>();
            services.AddTransient<IWalletApplication, WalletApplication>();
        }
    }
}
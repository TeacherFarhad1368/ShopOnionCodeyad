using Blogs.Query;
using Comments.Query;
using Microsoft.AspNetCore.Authentication.Cookies;
using Shared.Application.Services;
using Shared.Application.Services.Auth;
using ShopBoloor.WebApplication.Services;
using Site.Query;
using Users.Query;

namespace ShopBoloor.WebApplication.Utility
{
    public class DependencyBootstrapper
    {
        public static void Config(IServiceCollection services1,string connection)
        {
            services1.AddHttpContextAccessor();

            services1.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                x.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(x =>
                {
                    x.ExpireTimeSpan = TimeSpan.FromDays(30);
                    x.LoginPath = "/Auth/Login";
                    x.LogoutPath = "/Auth/Logout";
                    x.AccessDeniedPath = "/Auth/AccessDenied";
                });

           
            Blog_Bootstrapper.Config(services1, connection);
            User_Bootstrapper.Config(services1, connection);
            Comment_Bootstrapper.Config(services1, connection);
            Site_Bootstrapper.Config(services1, connection);

            services1.AddTransient<IFileService, FileService>();
            services1.AddTransient<IAuthService, AuthService>();
        }
    }
}

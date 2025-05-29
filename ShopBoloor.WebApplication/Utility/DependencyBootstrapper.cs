using Blogs.Query;
using Comments.Query;
using Emails.Query;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PostModule.Query;
using Query.Services;
using Seos.Query;
using Shared.Application.Services;
using Shared.Application.Services.Auth;
using ShopBoloor.WebApplication.Services;
using Site.Query;
using WebApp = Microsoft.AspNetCore.Builder.WebApplication;

namespace ShopBoloor.WebApplication.Utility
{
    public static class DependencyBootstrapper
    {
        public static void ConfigDependensies(this IServiceCollection services1,string connection)
        {
            services1.AddHttpClient();
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

            services1.AddControllersWithViews(x =>
            {
                x.CacheProfiles.Add("First", new Microsoft.AspNetCore.Mvc.CacheProfile
                {
                    Duration = 300
                });
                x.CacheProfiles.Add("Second", new Microsoft.AspNetCore.Mvc.CacheProfile
                {
                    Duration = 300,
                    Location = ResponseCacheLocation.Client
                });
                x.CacheProfiles.Add("Third", new Microsoft.AspNetCore.Mvc.CacheProfile
                {
                    Duration = 300,
                    VaryByQueryKeys = new string[] { "slug" }
                });
            });
            services1.AddMemoryCache(x =>
            {
                x.SizeLimit = 100;
            });
            services1.AddDistributedSqlServerCache(x =>
            {
                x.ConnectionString = connection;
                x.SchemaName = "dbo";
                x.TableName = "DistributedCaches";
                x.ExpiredItemsDeletionInterval = TimeSpan.FromMinutes(5);
                x.DefaultSlidingExpiration = TimeSpan.FromMinutes(5);   
            });
            services1.AddResponseCaching(x =>
            {
                //x.MaximumBodySize = 100;
                //x.SizeLimit = 200 * 1024 * 1024;
            });

            Modules_Bootstrapper.Config(services1, connection);


			services1.AddTransient<IFileService, FileService>();
            services1.AddTransient<IAuthService, AuthService>();
        }
        public static void RunWebApplication(this WebApp app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCaching();
            app.UseEndpoints(x =>
            {
                x.MapAreaControllerRoute(
                    name: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}",
                    areaName: "Admin"
                    );
                x.MapAreaControllerRoute(
                    name: "UserPanel",
                    pattern: "UserPanel/{controller=Home}/{action=Index}/{id?}",
                    areaName: "UserPanel"
                    );
                x.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");
            });


            app.Run();
        }
    }
}

using Blogs.Query;
using Microsoft.AspNetCore.DataProtection;
using ShopBoloor.WebApplication.Utility;
using Users.Query;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("Local");

DependencyBootstrapper.Config(services,connectionString);

services.AddControllersWithViews();

var app = builder.Build();
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

app.UseEndpoints(x =>
{
    x.MapAreaControllerRoute(
        name: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}",
        areaName: "Admin"
        );

    x.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");
});


app.Run();

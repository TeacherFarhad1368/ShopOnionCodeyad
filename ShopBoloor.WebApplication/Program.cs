using Blogs.Query;
using Microsoft.AspNetCore.DataProtection;
using ShopBoloor.WebApplication.Utility;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Users.Query;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("Local");

services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
services.ConfigDependensies(connectionString);
services.Configure<SiteData>(configuration.GetSection("SiteData"));

WebApplication app = builder.Build();
app.RunWebApplication();


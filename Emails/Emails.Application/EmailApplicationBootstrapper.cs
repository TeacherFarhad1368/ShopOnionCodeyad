using Emails.Application.Contract.EmailUserApplication.Command;
using Emails.Application.Contract.MessageUserApplication.Command;
using Emails.Application.Contract.SensEmailApplication.Command;
using Emails.Application.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Emails.Application
{
    public static class EmailApplicationBootstrapper
    {
        public static void Config(IServiceCollection services)
        {
            services.AddTransient<IEmailUserApplication, EmailUserApplication>();
            services.AddTransient<IMessageUserApplication, MessageUserApplication>();
            services.AddTransient<ISensEmailApplication, SensEmailApplication>();
        }
    }
}

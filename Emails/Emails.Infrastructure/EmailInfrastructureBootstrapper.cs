using Emails.Application;
using Emails.Domain.EmailUserAgg;
using Emails.Domain.MessageUserAgg;
using Emails.Domain.SendEmailAgg;
using Emails.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emails.Infrastructure
{
	public static class EmailInfrastructureBootstrapper
	{
		public static void Config(IServiceCollection services , string connection)
		{
			EmailApplicationBootstrapper.Config(services);
			services.AddTransient<IEmailUserRepository, EmailUserRepository>();
			services.AddTransient<ISendEmailRepository, SendEmailRepository>();
			services.AddTransient<IMessageUserRepository, MessageUserRepository>();

			services.AddDbContext<EmailContext>(x =>
			{
				x.UseSqlServer(connection);
			});
		}
	}
}

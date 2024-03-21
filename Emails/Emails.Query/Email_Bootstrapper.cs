using Emails.Application.Contract.SensEmailApplication.Query;
using Emails.Infrastructure;
using Emails.Query.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emails.Query
{
	public static class Email_Bootstrapper
	{
		public static void Config(IServiceCollection services,string connection)
		{
			EmailInfrastructureBootstrapper.Config(services, connection);
			services.AddTransient<ISendEmailQuery,SendEmailQuery>();	
		}
	}
}

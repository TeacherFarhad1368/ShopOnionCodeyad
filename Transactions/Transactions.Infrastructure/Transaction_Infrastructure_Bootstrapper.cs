using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Transactions.Application;
using Transactions.Domain;

namespace Transactions.Infrastructure
{
    public class Transaction_Infrastructure_Bootstrapper
    {
        public static void Config(IServiceCollection services,string connectionString)
        {

            services.AddTransient<ITransactionRepository, TransactionRepository>();

            Transaction_Application_Bootstrapper.Config(services);


            services.AddDbContext<TransactionContext>(x =>
            {
                x.UseSqlServer(connectionString);
            });
        }
    }
}

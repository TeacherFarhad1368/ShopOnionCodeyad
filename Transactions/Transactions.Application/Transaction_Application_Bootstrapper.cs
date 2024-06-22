using Microsoft.Extensions.DependencyInjection;
using Transactions.Application.Contract;

namespace Transactions.Application
{
    public class Transaction_Application_Bootstrapper
    {
        public static void Config(IServiceCollection services)
        {
            services.AddTransient<ITransactionApplication, TransactionApplication>();
        }
    }
}

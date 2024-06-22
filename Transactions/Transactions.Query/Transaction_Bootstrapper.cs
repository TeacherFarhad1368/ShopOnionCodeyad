using Microsoft.Extensions.DependencyInjection;
using Transactions.Infrastructure;

namespace Transactions.Query
{
    public class Transaction_Bootstrapper
    {
        public static void Config(IServiceCollection services, string connectionString)
        {
            Transaction_Infrastructure_Bootstrapper.Config(services, connectionString); 
        }
    }
}

using Shared.Domain;
namespace Transactions.Domain
{
    public interface ITransactionRepository : IRepository<long, Transaction> { };
}

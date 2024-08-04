using Shared.Application;
using Shared.Domain;

namespace Transactions.Domain
{
    public interface ITransactionRepository : IRepository<long, Transaction> {
        Task<long> CreateAsyncReturnKey(Transaction transaction); 
    };
}

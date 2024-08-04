using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using Transactions.Domain;

namespace Transactions.Infrastructure
{
    internal class TransactionRepository : Repository<long, Transaction>, ITransactionRepository
    {
        private readonly TransactionContext _context;
        public TransactionRepository(TransactionContext context) : base(context)
        {
            _context = context;
        }

        public async Task<long> CreateAsyncReturnKey(Transaction transaction)
        {
            if (await CreateAsync(transaction))
                return transaction.Id;
            return 0;
        }
    }
}

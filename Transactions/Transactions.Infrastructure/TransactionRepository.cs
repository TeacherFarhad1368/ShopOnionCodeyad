using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using Transactions.Domain;

namespace Transactions.Infrastructure
{
    internal class TransactionRepository : Repository<long, Transaction>, ITransactionRepository
    {
        public TransactionRepository(DbContext context) : base(context)
        {
        }
    }
}

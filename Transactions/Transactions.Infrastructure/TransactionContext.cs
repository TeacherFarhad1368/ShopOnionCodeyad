using Microsoft.EntityFrameworkCore;
using Transactions.Domain;

namespace Transactions.Infrastructure
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        {
            
        }
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TranactionConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}

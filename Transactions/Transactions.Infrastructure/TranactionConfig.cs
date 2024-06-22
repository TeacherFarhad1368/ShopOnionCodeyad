using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transactions.Domain;

namespace Transactions.Infrastructure
{
    internal class TranactionConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Domain.Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.RefId).IsRequired(false).HasMaxLength(100);

            builder.Property(b => b.Portal).IsRequired();
            builder.Property(b => b.TransactionFor).IsRequired();
            builder.Property(b => b.Status).IsRequired();

        }
    }
}

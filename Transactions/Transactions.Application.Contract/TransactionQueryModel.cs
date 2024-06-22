using Shared.Domain.Enum;

namespace Transactions.Application.Contract
{
    public class TransactionQueryModel
    {
        public TransactionQueryModel(long id, int userId, int price, string refId, TransactionPortal portal, TransactionStatus status, TransactionFor transactionFor, int ownerId)
        {
            Id = id;
            UserId = userId;
            Price = price;
            RefId = refId;
            Portal = portal;
            Status = status;
            TransactionFor = transactionFor;
            OwnerId = ownerId;
        }

        public long Id { get; private set; }
        public int UserId { get; private set; }
        public int Price { get; private set; }
        public string RefId { get; private set; }
        public TransactionPortal Portal { get; private set; }
        public TransactionStatus Status { get; private set; }
        public TransactionFor TransactionFor { get; private set; }
        public int OwnerId { get; private set; }
    }
}

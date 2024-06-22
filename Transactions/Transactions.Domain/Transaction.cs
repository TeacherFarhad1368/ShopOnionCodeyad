using Shared.Domain;
using Shared.Domain.Enum;
namespace Transactions.Domain
{
    public class Transaction :BaseEntityCreate<long>
    {
        public Transaction(int userId, int price, TransactionPortal portal)
        {
            UserId = userId;
            Price = price;
            RefId = "";
            Portal = portal;
            Status = TransactionStatus.نا_موفق;
        }
        public void Payment(TransactionStatus status,string refId)
        {
            Status = status;
            RefId = refId;
        }
        public int UserId { get; private set; }
        public int Price { get; private set; }
        public string RefId { get; private set; }
        public TransactionPortal Portal { get; private set; }
        public TransactionStatus Status { get; private set; }
    }
}

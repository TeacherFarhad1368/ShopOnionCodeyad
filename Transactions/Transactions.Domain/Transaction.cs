using Shared.Domain;
using Shared.Domain.Enum;
namespace Transactions.Domain
{
    public class Transaction : BaseEntityCreate<long>
    {
        public Transaction(int userId, int price, TransactionPortal portal,TransactionFor transactionFor,int ownerId,string authority)
        {
            UserId = userId;
            Price = price;
            RefId = "";
            Portal = portal;
            Status = TransactionStatus.نا_موفق;
            TransactionFor = transactionFor;
            OwnerId = ownerId;
            Authority = authority;
        }
        public void AddWalletId(int walletId)
        {
            OwnerId = walletId; 
        }
        public void Payment(TransactionStatus status,string refId)
        {
            Status = status;
            RefId = refId;
        }
        public int UserId { get; private set; }
        public int Price { get; private set; }
        public string RefId { get; private set; }
        public string Authority { get; private set; }
        public TransactionPortal Portal { get; private set; }
        public TransactionStatus Status { get; private set; }
        public TransactionFor TransactionFor { get; private set; }
        public int OwnerId { get; private set; }
    }
}

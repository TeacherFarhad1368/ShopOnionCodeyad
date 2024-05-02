using Shared.Domain;
using Shared.Domain.Enum;

namespace PostModule.Domain.UserPostAgg
{
    public class PostOrder : BaseEntityCreate<int>
    {
        public int UserId { get; private set; }
        public int TransactionId { get; private set; }
        public int PackageId { get; private set; }
        public int Price { get; private set; }
        public PostOrderStatus Status { get; private set; }
        public Package Package { get; private set; }
        public PostOrder(int packageId,int userId, int price)
        {

            PackageId = packageId;
            UserId = userId;
            Status = PostOrderStatus.پرداخت_نشده;
            TransactionId = 0;
            Price = price;

        }
        public void Edit(int packageId, int price)
        {

            PackageId = packageId;
            Price = price;

        }
        public void SuccessPayment(int transactionId)
        {
            TransactionId = transactionId;
            Status = PostOrderStatus.پرداخت_شده;
        }
        public PostOrder()
        {
            Package = new();
        }
    }
}

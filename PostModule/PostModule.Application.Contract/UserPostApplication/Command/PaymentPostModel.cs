namespace PostModule.Application.Contract.UserPostApplication.Command;

public class PaymentPostModel
{
    public PaymentPostModel(int userId, int transactionId, int price)
    {
        UserId = userId;
        TransactionId = transactionId;
        Price = price;
    }

    public int UserId { get; private set; }
    public int TransactionId { get; private set; }
    public int Price { get; private set; }
}
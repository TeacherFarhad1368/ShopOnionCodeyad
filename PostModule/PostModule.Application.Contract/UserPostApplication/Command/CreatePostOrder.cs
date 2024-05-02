namespace PostModule.Application.Contract.UserPostApplication.Command;

public class CreatePostOrder
{
    public CreatePostOrder(int userId, int packageId,int price)
    {
        UserId = userId;
        PackageId = packageId;
        Price = price;
    }

    public int UserId { get; private set; }
    public int PackageId { get; private set; }
    public int Price { get; private set; }
}
namespace Shop.Application.Contract.WishListApplication.Command;

public class CreateWishList
{
    public CreateWishList(int productId, int userId)
    {
        ProductId = productId;
        UserId = userId;
    }

    public int ProductId { get; private set; }
    public int UserId { get; private set; }
}
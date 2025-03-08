using Shared.Domain;
using Shop.Domain.ProductAgg;

namespace Shop.Domain.WishListAgg;
public class WishList : BaseEntity<int>
{
    public int ProductId { get; private set; }
    public int UserId { get; private set; }
    public Product Product { get; private set; }
    public WishList()
    {
        Product = new Product();
    }

    public WishList(int productId, int userId)
    {
        ProductId = productId;
        UserId = userId;
    }
}

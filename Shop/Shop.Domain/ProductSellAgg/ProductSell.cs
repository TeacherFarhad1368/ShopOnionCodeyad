using Shared.Domain;
using Shared.Domain.Enum;
using Shop.Domain.OrderAgg;
using Shop.Domain.ProductAgg;
using Shop.Domain.SellerAgg;
namespace Shop.Domain.ProductSellAgg;
public class ProductSell : BaseEntityCreateUpdateActive<int>
{
    public int ProductId { get; private set; }
    public int SellerId { get; private set; }
    public int Price { get; private set; }
    public int Amount { get; private set; }
    public string Unit { get; private set; }
    public int Weight { get; private set; }
    public Product Product { get; private set; }
    public Seller Seller { get; private set; }
    public List<OrderItem> OrderItems { get; private set; }
    public ProductSell()
    {
        Product = new();
        Seller = new();
        OrderItems = new();
    }

    public ProductSell(int productId, int sellerId, int price, string unit, int weight)
    {
        ProductId = productId;
        SellerId = sellerId;
        Price = price;
        Unit = unit;
        SetActivation(false);
        Weight = weight;
        Amount = 0;
    }
    public void Edit(int price, string unit, int weight)
    {
        Price = price;
        Unit = unit;
        Weight = weight;
        SetActivation(false);
    }
    public void ChangeAmount(int amount, StoreProductType type)
    {
        switch (type)
        {
            case StoreProductType.افزایش:
                Amount = Amount + amount;
                break;
            case StoreProductType.کاهش:
                Amount = (Amount - amount) < 0 ? 0 : (Amount - amount);
                break;
            default:
                break;
        }
    }
}

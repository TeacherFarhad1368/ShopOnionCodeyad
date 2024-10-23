using Shared.Domain.Enum;

namespace Stores.Application.Contract.StoreApplication.Command;

public class CreateStore
{
    public CreateStore(int sellerId, string description, List<CreateStoreProduct> products)
    {
        SellerId = sellerId;
        Description = description;
        Products = products;
    }

    public int SellerId { get; private set; }
    public string Description { get; private set; }
    public List<CreateStoreProduct> Products { get; private set; }
}
public class CreateStoreProduct
{
    public CreateStoreProduct(int productSellId, StoreProductType type, int count)
    {
        ProductSellId = productSellId;
        Type = type;
        Count = count;
    }

    public int ProductSellId { get; private set; }
    public StoreProductType Type { get; private set; }
    public int Count { get; private set; }
}

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
    public CreateStore()
    {
        
    }
    public int SellerId { get; set; }
    public string Description { get; set; }
    public List<CreateStoreProduct> Products { get; set; }
}
public class CreateStoreProduct
{
    public CreateStoreProduct(int productSellId, StoreProductType type, int count)
    {
        ProductSellId = productSellId;
        Type = type;
        Count = count;
    }
    public CreateStoreProduct()
    {
        
    }
    public int ProductSellId { get; set; }
    public StoreProductType Type { get; set; }
    public int Count { get; set; }
}

using Shared.Domain;
using Stores.Domain.StoreProductAgg;
namespace Stores.Domain.StoreAgg;
public class Store : BaseEntityCreate<int>
{
    public int SellerId { get; private set; }
    public string Description { get; private set; }
    public List<StoreProduct> StoreProducts { get; private set; }
    public Store()
    {
        StoreProducts = new();
    }
    public void EditDescription(string des)
    {
        Description = des;
    }
    public Store(int sellerId, string description)
    {
        SellerId = sellerId;
        Description = description;
    }
}

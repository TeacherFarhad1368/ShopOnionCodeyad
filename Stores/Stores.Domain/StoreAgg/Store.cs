using Shared.Domain;
using Stores.Domain.StoreProductAgg;
namespace Stores.Domain.StoreAgg;
public class Store : BaseEntityCreate<int>
{
    public int UserId { get; private set; }
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
    public Store(int userId,int sellerId, string description)
    {
        UserId = userId;
        SellerId = sellerId;
        Description = description;
    }
}

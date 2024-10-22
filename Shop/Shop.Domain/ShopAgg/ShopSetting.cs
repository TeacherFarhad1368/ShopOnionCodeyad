using Shared.Domain;
namespace Shop.Domain.ShopAgg;

public class ShopSetting : BaseEntity<int>
{
    public ShopSetting(int sellerDefault)
    {
        SellerDefault = sellerDefault;
    }
    public void Edit(int sellerDefault)
    {
        SellerDefault = sellerDefault;
    }

    public int SellerDefault { get; private set; }
}

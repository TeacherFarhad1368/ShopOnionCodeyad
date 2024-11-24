using Shared;

namespace Query.Contract.UserPanel.Seller;

public class SellerProductPageUserPanelQueryModel : BasePaging
{
    public string Filter { get; set; }
    public int SellerId { get; set; }
    public string SellerTitle { get; set; }
    public List<ProductSellUserPanelQueryModel> Products { get; set; }
}

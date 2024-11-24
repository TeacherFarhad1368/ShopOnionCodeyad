namespace Query.Contract.UserPanel.Seller;

public class ProductSellUserPanelQueryModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductTitle { get; set; }
    public string ProductSlug { get; set; }
    public string ProductImageName { get; set; }
    public int SellerId { get; set; }
    public int Price { get; set; }
    public int Amount { get; set; }
    public string Unit { get; set; }
    public int Weight { get; set; }
    public int SellCount { get; set; }
    public bool Active { get; set; }
}
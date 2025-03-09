namespace Query.Contract.UI.Product;

public class ProductSellForProductSingleQueryModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int SellerId { get; set; }
    public int Price { get; set; }
    public int PriceAfterOff { get; set; }
    public int Amount { get; set; }
    public string Unit { get; set; }
    public int Weight { get; set; }
    public string SellerName { get; set; }
    public string SellerAddress { get; set; }
}
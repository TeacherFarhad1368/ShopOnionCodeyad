namespace Query.Contract.Admin.Order;

public class OrderItemDetailForAdminQueryModel
{
    public int Id { get; set; }
    public int OrderSellerId { get; set; }
    public int ProductSellId { get; set; }
    public int ProductId { get; set; }
    public string ProductImageName { get; set; }
    public string ProductTitle { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int PriceAfterOff { get; set; }
    public int SumPrice { get; set; }
    public int SumPriceAfterOff { get; set; }
    public string Unit { get; set; }
}

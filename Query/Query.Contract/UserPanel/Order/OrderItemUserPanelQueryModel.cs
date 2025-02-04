namespace Query.Contract.UserPanel.Order;

public class OrderItemUserPanelQueryModel
{
    public int Id { get; set; }
    public string Unit { get; set; }
    public int ProductSellId { get; set; }
    public int ProductId { get; set; }
    public string ProductTitle { get; set; }
    public string ProductImageAddress { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int PriceAfterOff { get; set; }
    public int SumPrice { get; set; }
    public int SumPriceAfterOff { get; set; }
}
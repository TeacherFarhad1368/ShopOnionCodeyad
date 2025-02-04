namespace Query.Contract.UserPanel.Order;

public class OrderSellerUserPanelQueryModel
{
    public int Id { get; set; }
    public int SellerId { get; set; }
    public string SellerTitle { get; set; }
    public int DiscountId { get; set; }
    public int DiscountPercent { get; set; }
    public string DiscountTitle { get; set; }
    public int PostPrice { get; set; }
    public int Price { get; set; }
    public int PriceAfterOff { get; set; }
    public int PaymentPrice { get; set; }
    public int DiscountPrice { get; set; }
    public List<OrderItemUserPanelQueryModel> OrderItems { get; set; }
}

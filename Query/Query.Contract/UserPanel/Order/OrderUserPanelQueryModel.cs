using Shared.Domain.Enum;

namespace Query.Contract.UserPanel.Order;

public class OrderUserPanelQueryModel
{
    public int OrderId { get; set; }
    public OrderPayment OrderPayment { get; set; }
    public int OrderAddressId { get; set; }
    public int PostId { get; set; }
    public string? PostTitle { get; set; }
    public int DiscountId { get; set; }
    public int DiscountPercent { get; set; }
    public int Price { get; set; }
    public int PriceAfterOff { get; set; }
    public int PaymentPriceSeller { get; set; }
    public int PostPrice { get; set; }
    public int PaymentPrice { get; set; }
    public OrderAddressForOrderUserPanelQueryModel? OrderAddress { get; set; }
    public List<OrderSellerUserPanelQueryModel> Ordersellers { get; set; }
}

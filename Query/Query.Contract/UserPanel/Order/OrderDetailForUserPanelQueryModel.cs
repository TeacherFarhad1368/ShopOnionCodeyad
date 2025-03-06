using Shared.Domain.Enum;

namespace Query.Contract.UserPanel.Order;

public class OrderDetailForUserPanelQueryModel
{
    public int Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public OrderPayment OrderPayment { get; set; }
    public int DiscountPercent { get; set; }
    public string DiscountTitle { get; set; } 
    public int Price { get; set; }
    public int PriceAfterOff { get; set; }
    public int PostPrice { get; set; }
    public int PaymentPrice { get; set; }
    public int PaymentPriceSeller { get; set; }
    public string UpdateDate { get; set; }
    public OrderDetailAddressUserPanelQueryModel OrderAddress { get; set; }
    public List<OrderSellerDetailForUserPanelQueryModel> OrderSellers { get; set; }
}

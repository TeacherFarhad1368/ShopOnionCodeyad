using Shared.Domain.Enum;

namespace Query.Contract.Admin.Order;

public class OrderDetailForAdminQueryModel
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
    public OrderdetailUserInfoForAdminQueryModel User { get; set; }
    public OrderDetailAddressAdminQueryModel OrderAddress { get; set; }
    public List<OrderSellerDetailForAdminQueryModel> OrderSellers { get; set; }
}

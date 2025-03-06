using Shared.Domain.Enum;

namespace Query.Contract.Admin.Order;

public class OrderSellerDetailForAdminQueryModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int SellerId { get; set; }
    public string SellerAddress { get; set; }
    public OrderSellerStatus Status { get; set; }
    public int DiscountId { get; set; }
    public int DiscountPercent { get; set; }
    public int DiscountPrice { get; set; }
    public string DiscountTitle { get; set; }
    public int PostId { get; set; }
    public string? PostTitle { get; set; }
    public int PostPrice { get; set; }
    public int Price { get; set; }
    public int PriceAfterOff { get; set; }
    public int PaymentPrice { get; set; }
    public List<OrderItemDetailForAdminQueryModel> OrderItems { get; set; }
}

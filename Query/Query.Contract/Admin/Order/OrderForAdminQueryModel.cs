using Shared.Domain.Enum;

namespace Query.Contract.Admin.Order;

public class OrderForAdminQueryModel
{
    public int Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public int DiscountPercent { get; set; }
    public int DiscountId { get; set; }
    public string DiscountTitle { get; set; }
    public int Price { get; set; }
    public int PriceAfterOff { get; set; }
    public int PostPrice { get; set; }
    public int PaymentPrice { get; set; }
    public int PaymentPriceSeller { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string CreationDate { get; set; }
    public string UpdateDate { get; set; }
    public string BackgroundColor { get; set; }
}
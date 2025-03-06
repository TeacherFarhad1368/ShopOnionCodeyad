using Shared;
using Shared.Domain.Enum;

namespace Query.Contract.UserPanel.Order.Seller;
public interface IOrderSellerUserPanelQuery
{
    OrderSellerPaging GetOrderSellersForSeller(int userId, int pageId, int take);
    OrderSellerDetailForSellerPanelQueryModel GetOrderSellerDetailForSellerPanel(int orderSellerId, int userId);
}
public class OrderSellerPaging : BasePaging
{
    public List<OrderSellerQueryModel> OrderSellers { get; set; }
}
public class OrderSellerQueryModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int SellerId { get; set; }
    public string SellerName { get; set; }
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
    public string UpdateDate { get; set; }
}
public class OrderSellerDetailForSellerPanelQueryModel
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
    public string CreationDate { get; set; }
    public string UpdateDate { get; set; }
    public List<OrderItemDetailForSellerPanelQueryModel> OrderItems { get; set; }
    public OrderDetailAddressUserPanelQueryModel OrderAddress { get; set; }
}
public class OrderItemDetailForSellerPanelQueryModel
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

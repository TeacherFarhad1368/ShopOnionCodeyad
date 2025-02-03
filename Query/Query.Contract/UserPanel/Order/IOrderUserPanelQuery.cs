using Shared.Domain.Enum;

namespace Query.Contract.UserPanel.Order;
public interface IOrderUserPanelQuery
{
    Task<OrderUserPanelQueryModel> GetOpenOrderForUserAsync(int userId);    
}
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
public class OrderAddressForOrderUserPanelQueryModel
{
    public int Id { get; set; }
    public int StateId { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; }
    public string StateName { get; set; }
    public string AddressDetail { get; set; }
    public string PostalCode { get; set; }
    public string Phone { get; set; }
    public string FullName { get; set; }
    public string IranCode { get; set; }
    public string CreationDate { get; set; }
}
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
    public List<OrderItemUserPanelQueryModel> OrderItems { get; set; }
}
public class OrderItemUserPanelQueryModel
{
    public int Id { get; set; }
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
using Shared.Application;
using Shared.Domain.Enum;

namespace Shop.Application.Contract.OrderApplication.Command;
public interface IOrderApplication
{
    Task<bool> AddOrderDiscountAsync(int userId, int id, string title, int percent);
    Task<OperationResult> AddOrderItemAsync(int userId, int id);
    Task<bool> AddOrderSellerDiscountAsync(int userId, int sellerId, int discountId, string title,
        int percent);
    Task<bool> AddPostToOrdersellerAsync(int userId, int id, int postId, int price, string title);
    Task<OperationResult> ChangeOrderPayment(int userId, OrderPayment pay);
    Task CheckOrderEmpty(int userId);
    Task<OperationResult> CreateOrderAddressAsync(CreateOrderAddress model, int userId);
    Task<bool> DeleteOrderItemAsync(int id, int userId);
    Task<OperationResult> OrderItemMinus(int id, int userId);
    Task<OperationResult> OrderItemPlus(int id, int userId);
    Task<bool> UbsertOpenOrderForUserAsync(int _userId,List<ShopCartViewModel> cart);
    Task<bool> PaymentSuccessOrderAsync(int userId, int price);
    Task<bool> ChnageOrderSellerStatusBySellerAsync(int orderSellerId, OrderSellerStatus status, int userId);
}

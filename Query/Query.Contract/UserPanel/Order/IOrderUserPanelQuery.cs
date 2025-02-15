using Shared.Application;
using Shop.Application.Contract.OrderApplication.Command;

namespace Query.Contract.UserPanel.Order;
public interface IOrderUserPanelQuery
{
    Task<PostPriceForOpenOrder> CalculatePostPrices(int userId);
    Task CheckOrderItemDataAsync(int userId);
    Task<OrderUserPanelQueryModel> GetOpenOrderForUserAsync(int userId);
    Task<List<ShopCartViewModel>> GetOpenOrderItemsAsync(int userId);
    Task<bool> HaveUserOpenOrderAsync(int userId);
    Task<OperationResultWithKey> HaveUserOpenOrderSellerAsyncByOrderSellerIdAsync(int userId, int id);
}
public class PostPriceForOpenOrder
{
    public string Title { get; set; }
    public string Status { get; set; }
    public int Price { get; set; }
}
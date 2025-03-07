using Shared.Application;
using Shop.Application.Contract.OrderApplication.Command;

namespace Query.Contract.UserPanel.Order;
public interface IOrderUserPanelQuery
{
    Task<int> CalculateOrdersellerWeight(int id);
    Task CheckOrderItemDataAsync(int userId);
    Task<int> GetCityOfSeller(int sellerId);
    Task<OrderUserPanelQueryModel> GetOpenOrderForUserAsync(int userId);
    Task<List<ShopCartViewModel>> GetOpenOrderItemsAsync(int userId);
    OrderDetailForUserPanelQueryModel GetOrderDetailForUserPanel(int id, int userId);
    OrderDetailForUserPanelQueryModel GetOrderDetailForUserPanel(int id);
    Task<bool> HaveUserOpenOrderAsync(int userId);
    Task<OperationResultWithKey> HaveUserOpenOrderSellerAsyncByOrderSellerIdAsync(int userId, int id);
    Task<bool> IsOpenOrderSellerForUser(int id, int userId);
    OrderUserPanelPaging GetOrdersForUserPanel(int userId, int pageId, int take);
}

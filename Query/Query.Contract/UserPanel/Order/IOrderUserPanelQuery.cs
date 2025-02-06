using Shared.Application;
using Shop.Application.Contract.OrderApplication.Command;

namespace Query.Contract.UserPanel.Order;
public interface IOrderUserPanelQuery
{
    Task CheckOrderItemDataAsync(int userId);
    Task<OrderUserPanelQueryModel> GetOpenOrderForUserAsync(int userId);
    Task<List<ShopCartViewModel>> GetOpenOrderItemsAsync(int userId);
    Task<bool> HaveUserOpenOrderAsync(int userId);
    Task<OperationResultWithKey> HaveUserOpenOrderSellerAsyncByOrderSellerIdAsync(int userId, int id);
}

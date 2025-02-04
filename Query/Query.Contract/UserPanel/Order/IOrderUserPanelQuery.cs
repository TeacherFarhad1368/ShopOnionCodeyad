using Shared.Application;

namespace Query.Contract.UserPanel.Order;
public interface IOrderUserPanelQuery
{
    Task CheckOrderItemDataAsync(int userId);
    Task<OrderUserPanelQueryModel> GetOpenOrderForUserAsync(int userId);
    Task<bool> HaveUserOpenOrderAsync(int userId);
    Task<OperationResultWithKey> HaveUserOpenOrderSellerAsyncByOrderSellerIdAsync(int userId, int id);
}

using Shared.Application;
using Shared.Domain;
namespace Shop.Domain.OrderAgg;
public interface IOrderRepository : IRepository<int, Order> 
{
    Task CheckOrderEmpty(int userId);
    Task<int> CreateOrderaddressReturnKey(OrderAddress orderAddress);
    Task<bool> DeleteOrderItemAsync(int id, int userId);
    Task<Order> GetOpenOrderForUserAsync(int userId);
    Task<OrderAddress> GetOrderAddressByIdAsync(int orderAddressId);
    Task<OperationResult> OrderItemMinus(int id, int userId);
    Task<OperationResult> OrderItemPlus(int id, int userId);
}

using Shared.Application;
using Shared.Domain.Enum;

namespace Discounts.Application.Contract.OrderDiscountApplication.Command;
public interface IOrderDiscountApplication
{
    Task<OperationResult> CreateAsync(CreateOrderDiscount command, OrderDiscountType type, int shopId);
    Task<OperationResult> EditAsync(EditOrderDiscount command);
    Task<EditOrderDiscount> GetForEditAsync(int id);
    Task<bool> UseOrderDiscount(int id);
}

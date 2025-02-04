using Shared.Application;
using Shared.Domain.Enum;

namespace Discounts.Application.Contract.OrderDiscountApplication.Command;
public interface IOrderDiscountApplication
{
    Task<OperationResult> CreateAsync(CreateOrderDiscount command, OrderDiscountType type, int shopId);
    Task<OperationResult> EditAsync(EditOrderDiscount command);
    Task<OperationResult> EditBySellerAsync(EditOrderDiscount command, List<int> sellerIds);
    Task<EditOrderDiscount> GetForEditAsync(int id);
    EditOrderDiscount GetForEditBySeller(int id, List<int> sellerIds);
    Task<OperationResultOrderDiscount> GetOrderDiscountForAddOrderSellerdiscountAsync(int id, string code);
    Task MinusUseAsync(int id);
    Task<bool> UseOrderDiscount(int id);
}

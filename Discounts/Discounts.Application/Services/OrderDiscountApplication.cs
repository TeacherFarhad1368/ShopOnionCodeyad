using Discounts.Application.Contract.OrderDiscountApplication.Command;
using Discounts.Domain.OrderDiscountAgg;
using Shared.Application;
using Shared.Domain.Enum;

namespace Discounts.Application.Services;
internal class OrderDiscountApplication : IOrderDiscountApplication
{
    private readonly IOrderDiscountRepository _orderDiscountRepository;

    public OrderDiscountApplication(IOrderDiscountRepository orderDiscountRepository)
    {
        _orderDiscountRepository = orderDiscountRepository;
    }

    public Task<OperationResult> CreateAsync(CreateOrderDiscount command, OrderDiscountType type, int shopId)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult> EditAsync(EditOrderDiscount command)
    {
        throw new NotImplementedException();
    }

    public Task<EditOrderDiscount> GetForEditAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UseOrderDiscount(int id, int count)
    {
        throw new NotImplementedException();
    }
}

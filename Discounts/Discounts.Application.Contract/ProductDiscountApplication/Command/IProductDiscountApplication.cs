using Shared.Application;

namespace Discounts.Application.Contract.ProductDiscountApplication.Command;
public interface IProductDiscountApplication
{
    Task<OperationResult> CreateProductDiscountAsync(CreateProductDiscount command);
    Task<CreateProductDiscount> GetForEditAsync(int productId, int productSellId);
}

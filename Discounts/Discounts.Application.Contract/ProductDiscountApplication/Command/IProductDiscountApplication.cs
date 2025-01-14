using Shared.Application;

namespace Discounts.Application.Contract.ProductDiscountApplication.Command;
public interface IProductDiscountApplication
{
    Task<OperationResult> CreateProductDiscountAsync(CreateProductDiscount command);
    Task<OperationResult> CreateProductSellDiscountAsync(CreateProductSellDiscount command,int productId);
    Task<OperationResult> EditAsync(EditProductDiscount command);
    Task<EditProductDiscount> GetForEditAsync(int id);
}

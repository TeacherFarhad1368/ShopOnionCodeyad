using Discounts.Application.Contract.ProductDiscountApplication.Command;
using Discounts.Domain.ProductDiscountAgg;
using Shared.Application;

namespace Discounts.Application.Services;
internal class ProductDiscountApplication : IProductDiscountApplication
{
    private readonly IProductDiscountRepository _productDiscountRepository;

    public ProductDiscountApplication(IProductDiscountRepository productDiscountRepository)
    {
        _productDiscountRepository = productDiscountRepository;
    }

    public Task<OperationResult> CreateProductDiscountAsync(CreateProductDiscount command)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult> CreateProductSellDiscountAsync(CreateProductSellDiscount command)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult> EditAsync(EditProductDiscount command)
    {
        throw new NotImplementedException();
    }

    public Task<EditProductDiscount> GetForEditAsync(int id)
    {
        throw new NotImplementedException();
    }
}

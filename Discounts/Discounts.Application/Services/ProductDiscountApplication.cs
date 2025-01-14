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

    public async Task<OperationResult> CreateProductDiscountAsync(CreateProductDiscount command)
    {
        DateTime startDate = command.StartDate.ToEnglishDateTime();
        DateTime endDate = command.StartDate.ToEnglishDateTime();
        ProductDiscount productDiscount = new ProductDiscount(command.ProductId,0,startDate,endDate,command.Percent);
        if (await _productDiscountRepository.CreateAsync(productDiscount)) return new(true);
        return new OperationResult(false,ValidationMessages.SystemErrorMessage,nameof(command.StartDate));
    }

    public async Task<OperationResult> CreateProductSellDiscountAsync(CreateProductSellDiscount command, int productId)
    {
        DateTime startDate = command.StartDate.ToEnglishDateTime();
        DateTime endDate = command.StartDate.ToEnglishDateTime();
        ProductDiscount productDiscount = new ProductDiscount(productId, command.ProductSellId, startDate, endDate, command.Percent);
        if (await _productDiscountRepository.CreateAsync(productDiscount)) return new(true);
        return new OperationResult(false, ValidationMessages.SystemErrorMessage, nameof(command.StartDate));
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

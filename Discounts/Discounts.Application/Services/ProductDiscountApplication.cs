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
        DateTime endDate = command.EndDate.ToEnglishDateTime();
        if (endDate.Date < DateTime.Now.Date || endDate.Date < startDate.Date) return new(false, "تاریخ پایان باید حد اقل امروز باشد .");
        if(command.Percent < 1 || command.Percent > 99) return new(false, "درصد تخفیف باید از 1 تا 99 باشد . .");
        ProductDiscount discount = await _productDiscountRepository.GetByProductSellIdForEditAsync(command.ProductSellId, command.ProductId);
        if (discount != null)
        {
            discount.Edit(startDate, endDate,command.Percent);
            if (await _productDiscountRepository.SaveAsync()) return new(true);
            return new OperationResult(false, ValidationMessages.SystemErrorMessage, nameof(command.StartDate));
        }
        else
        {

            ProductDiscount productDiscount = new ProductDiscount(command.ProductId, command.ProductSellId, startDate, endDate, command.Percent);
            if (await _productDiscountRepository.CreateAsync(productDiscount)) return new(true);
            return new OperationResult(false, ValidationMessages.SystemErrorMessage, nameof(command.StartDate));
        }
    }

    public async Task<CreateProductDiscount> GetForEditAsync(int productId,int productSellId)
    {
        ProductDiscount discount = await _productDiscountRepository.GetByProductSellIdForEditAsync(productSellId,productId);
        return new CreateProductDiscount
        {
            EndDate = discount == null ? DateTime.Now.AddDays(1).ToPersainDatePicker() : discount.EndDate.ToPersainDatePicker(),
            Percent = discount == null ? 0 : discount.Percent,
            ProductId = productId,
            ProductSellId = productSellId,
            StartDate = discount == null ? DateTime.Now.ToPersainDatePicker() : discount.StartDate.ToPersainDatePicker()
        };
    }
}

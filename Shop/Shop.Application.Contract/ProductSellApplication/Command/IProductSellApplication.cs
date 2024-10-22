using Shared.Application;
namespace Shop.Application.Contract.ProductSellApplication.Command;
public interface IProductSellApplication
{
	Task<OperationResult> CreateAsync(CreateProductSell command);
	Task<OperationResult> EditAsync(EditProductSell command);
	Task<EditProductSell> GetForEditAsync(int id);
	Task<bool> ActivationChangeAsync(int id);
}

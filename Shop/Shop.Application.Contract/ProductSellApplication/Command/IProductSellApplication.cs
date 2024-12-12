using Shared.Application;
using Shared.Domain.Enum;
namespace Shop.Application.Contract.ProductSellApplication.Command;
public interface IProductSellApplication
{
	Task<OperationResult> CreateAsync(CreateProductSell command);
	Task<OperationResult> EditAsync(EditProductSell command);
	Task<EditProductSell> GetForEditAsync(int id);
	Task<bool> ActivationChangeAsync(int id);
	Task<bool> EditProductSellAmountAsync(List<EditProdoctSellAmount> sels);
}
public class EditProdoctSellAmount
{
    public int SellId { get; set; }
    public int count { get; set; }
    public StoreProductType Type { get; set; }
}
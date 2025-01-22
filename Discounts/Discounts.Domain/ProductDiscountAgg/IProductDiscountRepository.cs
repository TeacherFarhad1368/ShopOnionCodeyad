using Shared.Domain;

namespace Discounts.Domain.ProductDiscountAgg
{
    public interface IProductDiscountRepository : IRepository<int, ProductDiscount>
    {
        Task<ProductDiscount> GetByProductSellIdForEditAsync(int productSellId, int productId);
    }
}

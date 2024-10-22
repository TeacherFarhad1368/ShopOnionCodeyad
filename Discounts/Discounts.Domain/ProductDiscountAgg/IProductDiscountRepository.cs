using Shared.Domain;

namespace Discounts.Domain.ProductDiscountAgg
{
    public interface IProductDiscountRepository : IRepository<int, ProductDiscount> { }
}

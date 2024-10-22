using Shared.Domain;

namespace Shop.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<int, ProductCategory>
    {
        Task<bool> CheckProductCategoriesExist(List<int> categoryids);
    }
}

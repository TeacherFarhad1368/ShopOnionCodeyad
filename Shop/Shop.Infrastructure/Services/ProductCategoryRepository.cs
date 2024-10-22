using Shared.Infrastructure;
using Shop.Domain.ProductCategoryAgg;

namespace Shop.Infrastructure.Services;

internal class ProductCategoryRepository : Repository<int, ProductCategory>, IProductCategoryRepository
{
    private readonly ShopContext _context;
    public ProductCategoryRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> CheckProductCategoriesExist(List<int> categoryids)
    {
        foreach (int id in categoryids)
        {
            var ok = await ExistByAsync(c=>c.Id == id);
            if (ok == false) return false;
        }
        return true;
    }
}

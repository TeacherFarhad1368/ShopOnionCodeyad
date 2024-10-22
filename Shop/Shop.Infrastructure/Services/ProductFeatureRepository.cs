using Shared.Infrastructure;
using Shop.Domain.ProductAgg;

namespace Shop.Infrastructure.Services;

internal class ProductFeatureRepository : Repository<int, ProductFeature>, IProductFeatureRepository
{
    private readonly ShopContext _context;
    public ProductFeatureRepository(ShopContext context) : base(context)
    {
        _context = context;
    }
}

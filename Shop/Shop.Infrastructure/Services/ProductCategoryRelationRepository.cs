using Shared.Infrastructure;
using Shop.Domain.ProductCategoryRelationAgg;

namespace Shop.Infrastructure.Services;

internal class ProductCategoryRelationRepository : Repository<int, ProductCategoryRelation>, IProductCategoryRelationRepository
{
    private readonly ShopContext _context;
    public ProductCategoryRelationRepository(ShopContext context) : base(context)
    {
        _context = context;
    }
}

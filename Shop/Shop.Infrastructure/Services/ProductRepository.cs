using Shared.Infrastructure;
using Shop.Domain.ProductAgg;

namespace Shop.Infrastructure.Services;

internal class ProductRepository : Repository<int, Product>, IProductRepository
{
    private readonly ShopContext _context;
    public ProductRepository(ShopContext context) : base(context)
    {
        _context = context;
    }
}

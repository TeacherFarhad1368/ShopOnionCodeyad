using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using Shop.Domain.ProductSellAgg;

namespace Shop.Infrastructure.Services;

internal class ProductSellRepository : Repository<int, ProductSell>, IProductSellRepository
{
    private readonly ShopContext _context;
    public ProductSellRepository(ShopContext context) : base(context)
    {
        _context = context;
    }
}

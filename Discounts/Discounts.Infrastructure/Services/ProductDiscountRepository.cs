using Discounts.Domain.ProductDiscountAgg;
using Shared.Infrastructure;
namespace Discounts.Infrastructure.Services;

internal class ProductDiscountRepository : Repository<int, ProductDiscount>, IProductDiscountRepository
{
    private readonly DiscountContext _context;
    public ProductDiscountRepository(DiscountContext context) : base(context)
    {
        _context = context;
    }
}
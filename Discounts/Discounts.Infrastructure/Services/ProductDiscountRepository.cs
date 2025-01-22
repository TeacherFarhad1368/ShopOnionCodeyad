using Discounts.Domain.ProductDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

namespace Discounts.Infrastructure.Services;

internal class ProductDiscountRepository : Repository<int, ProductDiscount>, IProductDiscountRepository
{
    private readonly DiscountContext _context;
    public ProductDiscountRepository(DiscountContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ProductDiscount> GetByProductSellIdForEditAsync(int productSellId,int productId)
    {
        return await _context.ProductDiscounts.OrderBy(p => p.Id).LastOrDefaultAsync(p => p.ProductSellId == productSellId && p.ProductId == productId);
    }
}
using Discounts.Domain.OrderDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

namespace Discounts.Infrastructure.Services;
internal class OrderDiscountRepository : Repository<int, OrderDiscount>, IOrderDiscountRepository
{
    private readonly DiscountContext _context;
    public OrderDiscountRepository(DiscountContext context) : base(context)
    {
        _context = context;
    }

    public async Task<OrderDiscount> GetByCodeAsync(string code) =>
        await _context.OrderDiscounts.SingleOrDefaultAsync(s => s.Code.Trim() == code.Trim());
}

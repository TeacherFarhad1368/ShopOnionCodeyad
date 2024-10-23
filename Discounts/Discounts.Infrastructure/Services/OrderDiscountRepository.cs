using Discounts.Domain.OrderDiscountAgg;
using Shared.Infrastructure;
namespace Discounts.Infrastructure.Services;
internal class OrderDiscountRepository : Repository<int, OrderDiscount>, IOrderDiscountRepository
{
    private readonly DiscountContext _context;
    public OrderDiscountRepository(DiscountContext context) : base(context)
    {
        _context = context;
    }
}

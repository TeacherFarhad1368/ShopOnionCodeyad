using Shared.Infrastructure;
using Shop.Domain.OrderAgg;
namespace Shop.Infrastructure.Services;
internal class OrderRepository : Repository<int, Order>, IOrderRepository
{
    private readonly ShopContext _context;
    public OrderRepository(ShopContext context) : base(context)
    {
        _context = context;
    }
}
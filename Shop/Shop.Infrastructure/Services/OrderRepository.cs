using Microsoft.EntityFrameworkCore;
using Shared.Domain.Enum;
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

    public async Task<Order> GetOpenOrderForUserAsync(int userId)
    {
        var order =
           await _context.Orders.Include(o => o.OrderSellers).ThenInclude(o => o.OrderItems)
            .SingleOrDefaultAsync(o => o.UserId == userId && o.OrderStatus == OrderStatus.پرداخت_نشده);
        if(order == null)
        {
            order = new(userId);
            await CreateAsync(order);
            return await _context.Orders.Include(o => o.OrderSellers).ThenInclude(o => o.OrderItems)
            .SingleOrDefaultAsync(o => o.UserId == userId && o.OrderStatus == OrderStatus.پرداخت_نشده);
        }
        else
        return order;
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Shared.Application;
using Shared.Domain.Enum;
using Shared.Infrastructure;
using Shop.Domain.OrderAgg;
using System.ComponentModel.Design;

namespace Shop.Infrastructure.Services;
internal class OrderRepository : Repository<int, Order>, IOrderRepository
{
    private readonly ShopContext _context;
    public OrderRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public async Task CheckOrderEmpty(int userId)
    {
        var order =
           await _context.Orders.Include(o => o.OrderSellers).ThenInclude(o => o.OrderItems)
            .SingleOrDefaultAsync(o => o.UserId == userId && o.OrderStatus == OrderStatus.پرداخت_نشده);
        if(order != null)
        {
            if (order.OrderSellers.Count == 0)
                _context.Orders.Remove(order);
            else
              foreach(var seller in order.OrderSellers)
                 if(seller.OrderItems.Count == 0)
                       _context.OrderSellers.Remove(seller);
            if (order.OrderSellers.Count == 0)
                _context.Orders.Remove(order);

            await SaveAsync();
        }
    }

    public async Task<bool> DeleteOrderItemAsync(int id, int userId)
    {
        var item = await _context.OrderItems.Include(i=>i.OrderSeller)
            .ThenInclude(s=>s.Order).SingleOrDefaultAsync(o=>o.Id == id && o.OrderSeller.Order.UserId == userId);
        if(item == null || item.OrderSeller.Order.OrderStatus != OrderStatus.پرداخت_نشده)
            return false;

        _context.OrderItems.Remove(item);
        return await SaveAsync();
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

    public async Task<OperationResult> OrderItemMinus(int id, int userId)
    {
        var item = await _context.OrderItems.Include(i => i.OrderSeller)
            .ThenInclude(s => s.Order).SingleOrDefaultAsync(o => o.Id == id && o.OrderSeller.Order.UserId == userId);
        if (item == null) return new(false, "موردی یافت نشد");
        if(item.Count == 1)
        {
            bool oh = await DeleteOrderItemAsync(id,userId);
            return new(oh);
        }
        else
        {

            item.MinusCount(1);
            await SaveAsync();
            return new(true);
        }
    }
    public async Task<OperationResult> OrderItemPlus(int id, int userId)
    {
        var item = await _context.OrderItems.Include(i => i.OrderSeller)
            .ThenInclude(s => s.Order).SingleOrDefaultAsync(o => o.Id == id && o.OrderSeller.Order.UserId == userId);
        if (item == null) return new(false, "موردی یافت نشد");
        var productSell = await _context.ProductSells.FindAsync(item.ProductSellId);
        if (productSell.Amount <= item.Count) return new(false, "موجودی  نداریم");
        item.PlusCount(1);
        await SaveAsync();
        return new(true);
    }
}
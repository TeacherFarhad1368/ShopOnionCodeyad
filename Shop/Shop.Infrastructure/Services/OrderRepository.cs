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

    public async Task<bool> ChnageOrderSellerStatusBySellerAsync(int orderSellerId, OrderSellerStatus status, int userId)
    {
        var orderSeller = await _context.OrderSellers.Include(s => s.Order).Include(s=>s.Seller)
            .SingleOrDefaultAsync(s => s.Id == orderSellerId);
        if (orderSeller == null || orderSeller.Seller.UserId != userId ) return false;
        switch (orderSeller.Status)
        {
            case OrderSellerStatus.پرداخت_شده:
                if (status == OrderSellerStatus.لغو_شده_توسط_فروشنده || status == OrderSellerStatus.در_حال_آماده_سازی)
                {
                    orderSeller.ChangeStatus(status);
                    return await SaveAsync();
                }
                break;
            case OrderSellerStatus.در_حال_آماده_سازی:
                if (status == OrderSellerStatus.لغو_شده_توسط_فروشنده || status == OrderSellerStatus.ارسال_شده)
                {
                    orderSeller.ChangeStatus(status);
                    return await SaveAsync();
                }
                break;
        }
        return false;
    }

    public async Task<int> CreateOrderaddressReturnKey(OrderAddress orderAddress)
    {
        await _context.OrderAddresses.AddAsync(orderAddress);
        await SaveAsync();
        return orderAddress.Id;
    }

    public async Task<bool> DeleteOrderItemAsync(int id, int userId)
    {
        var order =
          await _context.Orders.Include(o => o.OrderSellers).ThenInclude(o => o.OrderItems)
           .SingleOrDefaultAsync(o => o.UserId == userId && o.OrderStatus == OrderStatus.پرداخت_نشده);
        if(order == null) return false; 
        foreach(var seller in order.OrderSellers)
            if(seller.OrderItems.Any(i=>i.ProductSellId == id))
            {
                var item1 = seller.OrderItems.Single(o => o.ProductSellId == id);
                _context.OrderItems.Remove(item1);
                seller.AddPostPrice(0, 0, "");
                return await SaveAsync();
            }
       
            return false;
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

    public async Task<OrderAddress> GetOrderAddressByIdAsync(int orderAddressId)=>
        await _context.OrderAddresses.SingleOrDefaultAsync(a => a.Id == orderAddressId);

    public async Task<OperationResult> OrderItemMinus(int id, int userId)
    {
        var item = await _context.OrderItems.Include(i => i.OrderSeller)
            .ThenInclude(s => s.Order).SingleOrDefaultAsync(o => o.Id == id && o.OrderSeller.Order.UserId == userId);
        if (item == null || item.OrderSeller.Order.OrderStatus != OrderStatus.پرداخت_نشده) return new(false, "موردی یافت نشد");
        if(item.Count == 1)
        {
            bool oh = await DeleteOrderItemAsync(id,userId);
            return new(oh);
        }
        else
        {

            item.MinusCount(1);
            item.OrderSeller.AddPostPrice(0, 0, "");
            await SaveAsync();
            return new(true);
        }
    }
    public async Task<OperationResult> OrderItemPlus(int id, int userId)
    {
        var item = await _context.OrderItems.Include(i => i.OrderSeller)
            .ThenInclude(s => s.Order).SingleOrDefaultAsync(o => o.Id == id && o.OrderSeller.Order.UserId == userId);
        if (item == null || item.OrderSeller.Order.OrderStatus != OrderStatus.پرداخت_نشده) return new(false, "موردی یافت نشد");
        var productSell = await _context.ProductSells.FindAsync(item.ProductSellId);
        if (productSell.Amount <= item.Count) return new(false, "موجودی  نداریم");
        item.PlusCount(1);
        item.OrderSeller.AddPostPrice(0, 0, "");
        await SaveAsync();
        return new(true);
    }
}
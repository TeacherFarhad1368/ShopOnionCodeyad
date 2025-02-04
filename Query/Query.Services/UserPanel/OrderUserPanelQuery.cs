using Discounts.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Contract.UserPanel.Order;
using Shared.Application;
using Shop.Infrastructure;
using Stores.Infrastructure;

namespace Query.Services.UserPanel;
internal class OrderUserPanelQuery : IOrderUserPanelQuery
{
    private readonly ShopContext _shopContext;  
    private readonly DiscountContext _discountContext;

    public OrderUserPanelQuery(ShopContext shopContext, DiscountContext discountContext)
    {
        _shopContext = shopContext;
        _discountContext = discountContext;
    }

    public async Task CheckOrderItemDataAsync(int userId)
    {
        var order = await _shopContext.Orders.Include(o => o.OrderSellers)
           .ThenInclude(s => s.OrderItems).SingleOrDefaultAsync(s => s.UserId == userId &&
           s.OrderStatus == Shared.Domain.Enum.OrderStatus.پرداخت_نشده);
        if (order != null)
        {
            foreach(var seller in order.OrderSellers)
            {
                foreach(var item in seller.OrderItems)
                {
                    var productSell = await _shopContext.ProductSells.Include(s => s.Product)
                        .SingleAsync(s => s.Id == item.ProductSellId);
                    int price = item.Price;
                    int priceAfterOff = item.PriceAfterOff;
                    string unit = productSell.Unit;
                    int count = item.Count;
                    if (count > productSell.Amount)
                        count = productSell.Amount;
                    if (_discountContext.ProductDiscounts.Any(p => p.ProductId == productSell.ProductId && 
                    p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date))
                    {
                        
                        var discount = _discountContext.ProductDiscounts.OrderByDescending(p => p.Percent)
                        .First(p => p.ProductId == productSell.ProductId && p.StartDate.Date <= DateTime.Now.Date && 
                        p.EndDate.Date >= DateTime.Now.Date);
                        if (discount.ProductSellId > 0)
                        {
                            price = productSell.Price;
                            priceAfterOff = productSell.Price - (discount.Percent * productSell.Price / 100);
                        }
                        else
                        {
                            priceAfterOff = productSell.Price - (discount.Percent * productSell.Price / 100);
                        }
                    }
                    item.Edit(count, price, priceAfterOff,unit);
                }
            }
            await _discountContext.SaveChangesAsync();
        }
    }

    public async Task<OrderUserPanelQueryModel> GetOpenOrderForUserAsync(int userId)
    {
        var order = await _shopContext.Orders.Include(o=>o.OrderSellers)
            .ThenInclude(s=>s.OrderItems).SingleOrDefaultAsync(s=>s.UserId == userId &&
            s.OrderStatus == Shared.Domain.Enum.OrderStatus.پرداخت_نشده);
        if (order == null) return null;
        OrderUserPanelQueryModel model = new()
        {
            OrderAddress = null,
            OrderAddressId = order.OrderAddressId,
            PriceAfterOff = order.PriceAfterOff,
            DiscountId = order.DiscountId,
            DiscountPercent = order.DiscountPercent,
            OrderId = order.Id,
            OrderPayment = order.OrderPayment,
            DiscountTitle = order.DiscountTitle,
            Ordersellers = order.OrderSellers.Select(s=> new OrderSellerUserPanelQueryModel
            {
                PriceAfterOff = s.PriceAfterOff,
                DiscountId= s.DiscountId,   
                DiscountPercent= s.DiscountPercent,
                Id = s.Id,
                DiscountTitle = s.DiscountTitle,    
                OrderItems = s.OrderItems.Select(i=> new OrderItemUserPanelQueryModel
                {
                    PriceAfterOff= i.PriceAfterOff,
                    SumPriceAfterOff= i.SumPriceAfterOff,
                    Count = i.Count,
                    Id = i.Id,
                    Price = i.Price,
                    ProductId = 0 ,
                    ProductSellId = i.ProductSellId,
                    ProductTitle = "",
                    SumPrice = i.SumPrice,
                    ProductImageAddress = FileDirectories.ProductImageDirectory500,
                    Unit = i.Unit
                }).ToList(),
                PaymentPrice = s.PaymentPrice,
                PostPrice = s.PostPrice,
                Price = s.Price,
                SellerId = s.SellerId,
                SellerTitle = "",
                DiscountPrice = s.DiscountPercent * s.PriceAfterOff / 100
            }).ToList(),
            PaymentPrice = order.PaymentPrice,
            PaymentPriceSeller = order.PaymentPriceSeller,
            PostId = order.PostId,  
            PostPrice = order.PostPrice,
            PostTitle = order.PostTitle,    
            Price = order.Price,
            DiscountPrice = order.DiscountPercent * order.PaymentPriceSeller / 100
        };
        foreach(var item in model.Ordersellers)
        {
            var seller = _shopContext.Sellers.Find(item.SellerId);
            item.SellerTitle = seller.Title;
            foreach(var p in item.OrderItems)
            {
                var sell = _shopContext.ProductSells.Find(p.ProductSellId);
                var product = _shopContext.Products.Find(sell.ProductId);
                p.ProductId = product.Id;
                p.ProductTitle = product.Title;
                p.ProductImageAddress = p.ProductImageAddress + product.ImageName;
            }
        }
        return model;   
    }

    public async Task<bool> HaveUserOpenOrderAsync(int userId) =>
        await _shopContext.Orders.AnyAsync(s => s.UserId == userId &&
            s.OrderStatus == Shared.Domain.Enum.OrderStatus.پرداخت_نشده);

    public async Task<OperationResultWithKey> HaveUserOpenOrderSellerAsyncByOrderSellerIdAsync(int userId, int id)
    {
        var order = await _shopContext.Orders.Include(o => o.OrderSellers)
          .ThenInclude(s => s.OrderItems).SingleOrDefaultAsync(s => s.UserId == userId &&
          s.OrderStatus == Shared.Domain.Enum.OrderStatus.پرداخت_نشده);
        bool ok = order != null && order.OrderSellers.Any(s => s.SellerId == id);
        if (ok) return new(true);
        return new(false, "فروشگاهی یافت نشد .");
    }
}

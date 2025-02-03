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
    private readonly StoreContext _storeContext;
    private readonly DiscountContext _discountContext;

    public OrderUserPanelQuery(ShopContext shopContext, StoreContext storeContext, DiscountContext discountContext)
    {
        _shopContext = shopContext;
        _storeContext = storeContext;
        _discountContext = discountContext;
    }

    public async Task<OrderUserPanelQueryModel> GetOpenOrderForUserAsync(int userId)
    {
        var order = await _shopContext.Orders.Include(o=>o.OrderSellers)
            .ThenInclude(s=>s.OrderItems).SingleOrDefaultAsync(s=>s.UserId == userId && s.OrderStatus == Shared.Domain.Enum.OrderStatus.پرداخت_نشده);
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
                    ProductImageAddress = FileDirectories.ProductImageDirectory500
                }).ToList(),
                PaymentPrice = s.PaymentPrice,
                PostPrice = s.PostPrice,
                Price = s.Price,
                SellerId = s.SellerId,
                SellerTitle = ""
            }).ToList(),
            PaymentPrice = order.PaymentPrice,
            PaymentPriceSeller = order.PaymentPriceSeller,
            PostId = order.PostId,  
            PostPrice = order.PostPrice,
            PostTitle = order.PostTitle,    
            Price = order.Price
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
}

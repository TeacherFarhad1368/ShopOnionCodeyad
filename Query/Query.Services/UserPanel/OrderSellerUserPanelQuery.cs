using Discounts.Infrastructure;
using PostModule.Infrastracture.EF;
using Query.Contract.UserPanel.Order.Seller;
using Shop.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Application;
using Query.Contract.UserPanel.Order;
using Shop.Domain.OrderAgg;
namespace Query.Services.UserPanel;
internal class OrderSellerUserPanelQuery : IOrderSellerUserPanelQuery
{
    private readonly ShopContext _shopContext;
    private readonly DiscountContext _discountContext;
    private readonly Post_Context _post_Context;
    public OrderSellerUserPanelQuery(ShopContext shopContext, DiscountContext discountContext, Post_Context post_Context)
    {
        _shopContext = shopContext;
        _discountContext = discountContext;
        _post_Context = post_Context;
    }

    public OrderSellerDetailForSellerPanelQueryModel GetOrderSellerDetailForSellerPanel(int orderSellerId, int userId)
    {
        var orderSeller = _shopContext.OrderSellers.Include(o => o.Order).Include(o => o.OrderItems)
            .ThenInclude(I => I.ProductSell).SingleOrDefault(s=>s.Id == orderSellerId);
        if (orderSeller == null) return null;
        var seller = _shopContext.Sellers.Find(orderSeller.SellerId);
        if (seller.UserId != userId) return null;
        OrderSellerDetailForSellerPanelQueryModel model = new()
        {
            PriceAfterOff = orderSeller.PriceAfterOff,
            SellerAddress = "",
            DiscountId = orderSeller.DiscountId,
            DiscountPercent = orderSeller.DiscountPercent,
            DiscountTitle = orderSeller.DiscountTitle,
            UserCustomerId = orderSeller.Order.UserId,
            Id = orderSeller.Id,
            DiscountPrice = orderSeller.DiscountPercent * orderSeller.PriceAfterOff / 100,
            OrderId = orderSeller.OrderId,
            OrderItems = orderSeller.OrderItems.Select(i => new OrderItemDetailForSellerPanelQueryModel
            {
                PriceAfterOff = i.PriceAfterOff,
                SumPriceAfterOff = i.SumPriceAfterOff,
                Count = i.Count,
                Id = i.Id,
                OrderSellerId = i.OrderSellerId,
                Price = i.Price,
                ProductId = 0,
                ProductImageName = "",
                ProductSellId = i.ProductSellId,
                ProductTitle = "",
                SumPrice = i.SumPrice,
                Unit = i.Unit
            }).ToList(),
            PaymentPrice = orderSeller.PaymentPrice,
            PostId = orderSeller.PostId,
            PostPrice = orderSeller.PostPrice,
            PostTitle = orderSeller.PostTitle,
            Price = orderSeller.Price,
            SellerId = orderSeller.SellerId,
            Status = orderSeller.Status,
            OrderAddress = null,
            CreationDate = orderSeller.Order.CreateDate.ToPersainDate(),
            UpdateDate = orderSeller.Order.UpdateDate.ToPersainDate()
        };
        var address = _shopContext.OrderAddresses.Single(o => o.Id == orderSeller.Order.OrderAddressId);
        var city = _post_Context.Cities.Include(c => c.State).Single(c => c.Id == address.CityId && c.StateId == address.StateId);
        model.OrderAddress = new()
        {
            AddressDetail = address.AddressDetail,
            City = city.Title,
            CityId = address.CityId,
            FullName = address.FullName,
            IranCode = address.IranCode,
            Phone = address.Phone,
            PostalCode = address.PostalCode,
            State = city.State.Title,
            StateId = address.StateId
        };
        var s = _shopContext.Sellers.Find(orderSeller.SellerId);
        model.SellerAddress = s.Title;
        foreach (var item in model.OrderItems)
        {
            var productSell = _shopContext.ProductSells.Include(p => p.Product).Single(s => s.Id == item.ProductSellId);
            item.ProductId = productSell.ProductId;
            item.ProductTitle = productSell.Product.Title;
            item.ProductImageName = $"{FileDirectories.ProductImageDirectory100}{productSell.Product.ImageName}";
        }
        return model;   
    }

    public OrderSellerPaging GetOrderSellersForSeller(int userId, int pageId, int take)
    {
        List<int> listSsllerIds = _shopContext.Sellers.Where(s=>s.UserId == userId).Select(s => s.Id).ToList();
        var res = _shopContext.OrderSellers.Include(o=>o.Order).Include(o=>o.OrderItems)
            .ThenInclude(I=>I.ProductSell).Where(o=> o.Status != Shared.Domain.Enum.OrderSellerStatus.پرداخت_نشده &&
            listSsllerIds.Contains(o.SellerId)).OrderByDescending(o=>o.Id);
        OrderSellerPaging model = new OrderSellerPaging();
        model.GetData(res,pageId, take,2);
        model.OrderSellers = new();
        if (res.Count() > 0)
            model.OrderSellers = res.Skip(model.Skip).Take(model.Take)
                .Select(o => new OrderSellerQueryModel
                {
                    PriceAfterOff = o.PriceAfterOff,
                    DiscountId = o.DiscountId,
                    DiscountPercent = o.DiscountPercent,
                    DiscountPrice = o.DiscountPercent * o.PriceAfterOff / 100,
                    DiscountTitle = o.DiscountTitle,
                    Id = o.Id,
                    OrderId = o.OrderId,
                    PaymentPrice = o.PaymentPrice,
                    PostId = o.PostId,
                    PostPrice = o.PostPrice,
                    PostTitle = o.PostTitle,
                    Price = o.Price,    
                    SellerId = o.SellerId,
                    Status = o.Status,
                    UpdateDate = o.Order.UpdateDate.ToPersainDate(),
                    SellerName = ""
                }).ToList();
        if (model.OrderSellers.Count() > 0)
            model.OrderSellers.ForEach(x =>
            {
                var seller = _shopContext.Sellers.Find(x.SellerId);
                x.SellerName = seller.Title;
            });
            return model;   
    }
}

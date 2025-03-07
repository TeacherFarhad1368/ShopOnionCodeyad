using Discounts.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PostModule.Infrastracture.EF;
using Query.Contract.UserPanel.Order;
using Shared.Application;
using Shop.Application.Contract.OrderApplication.Command;
using Shop.Domain.SellerAgg;
using Shop.Infrastructure;
using Stores.Infrastructure;

namespace Query.Services.UserPanel;
internal class OrderUserPanelQuery : IOrderUserPanelQuery
{
    private readonly ShopContext _shopContext;  
    private readonly DiscountContext _discountContext;
    private readonly Post_Context _post_Context;
    public OrderUserPanelQuery(ShopContext shopContext, DiscountContext discountContext, Post_Context post_Context)
    {
        _shopContext = shopContext;
        _discountContext = discountContext;
        _post_Context = post_Context;
    }

    public async Task<int> CalculateOrdersellerWeight(int id)
    {
        var seller = await _shopContext.OrderSellers.Include(s=>s.OrderItems).ThenInclude(i=>i.ProductSell)
            .SingleOrDefaultAsync(s=>s.Id == id);
        if (seller == null) return 0;
        int weight = 0;
        foreach(var item in seller.OrderItems)
        {
            int w = item.Count * item.ProductSell.Weight;
            weight=  weight + w;
        }
        return weight;
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
                    int price = productSell.Price;
                    int priceAfterOff = productSell.Price;
                    string unit = productSell.Unit;
                    int count = item.Count;
                    if (count > productSell.Amount)
                        count = productSell.Amount;


                    var discount1 = _discountContext.ProductDiscounts.OrderByDescending(p => p.Percent)
                        .FirstOrDefault(p => p.ProductId == productSell.ProductId && p.ProductSellId == productSell.Id && p.StartDate.Date <= DateTime.Now.Date &&
                        p.EndDate.Date >= DateTime.Now.Date);
                    if(discount1 != null)
                    {
                        priceAfterOff = productSell.Price - (discount1.Percent * productSell.Price / 100);
                    }
                    else
                    {
                        var discount2 = _discountContext.ProductDiscounts.OrderByDescending(p => p.Percent)
                        .FirstOrDefault(p => p.ProductId == productSell.ProductId && p.ProductSellId == 0 && p.StartDate.Date <= DateTime.Now.Date &&
                        p.EndDate.Date >= DateTime.Now.Date);
                        if(discount2 != null)
                        {
                            priceAfterOff = productSell.Price - (discount2.Percent * productSell.Price / 100);
                        }
                    }
                    item.Edit(count, price, priceAfterOff,unit);
                }
            }
            await _discountContext.SaveChangesAsync();
        }
    }

    public async Task<int> GetCityOfSeller(int sellerId)
    {
        var seller = await _shopContext.Sellers.SingleOrDefaultAsync(s => s.Id == sellerId);
        if (seller == null) return 0;
        return seller.CityId;
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
                PostId = s.PostId,
                PostTitle = s.PostTitle,
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
            PostPrice = order.PostPrice,
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
        if(order.OrderAddressId > 0)
        {
            var address = await _shopContext.OrderAddresses.SingleOrDefaultAsync(a=>a.Id == order.OrderAddressId);
            if(address != null)
            {
                var city = await _post_Context.Cities.Include(c=>c.State).SingleAsync(c=>c.Id == address.CityId);
                model.OrderAddress = new OrderAddressForOrderUserPanelQueryModel
                {
                    AddressDetail = address.AddressDetail,
                    CityId = address.CityId,
                    CityName = city.Title,
                    FullName = address.FullName,
                    Id = address.Id,
                    IranCode = address.IranCode,
                    Phone = address.Phone,
                    PostalCode = address.PostalCode,
                    StateId = address.StateId,
                    StateName = city.State.Title
                };
            }
        }
        return model;   
    }

    public async Task<List<ShopCartViewModel>> GetOpenOrderItemsAsync(int userId)
    {
        List<ShopCartViewModel> model = new();
        var order = await _shopContext.Orders.Include(o => o.OrderSellers)
           .ThenInclude(s => s.OrderItems).SingleOrDefaultAsync(s => s.UserId == userId &&
           s.OrderStatus == Shared.Domain.Enum.OrderStatus.پرداخت_نشده);
        if (order == null) return model;
        foreach(var seller in order.OrderSellers)
        {
            var shop = _shopContext.Sellers.Find(seller.SellerId);
            foreach (var item in seller.OrderItems)
            {
                
                var productSell = _shopContext.ProductSells.Include(p => p.Product).Single(p => p.Id == item.ProductSellId);
                model.Add(new ShopCartViewModel
                {
                    priceAfterOff = item.PriceAfterOff,
                    count = item.Count,
                    imageName = FileDirectories.ProductImageDirectory500 + productSell.Product.ImageName,
                    price = item.Price,
                    productId = productSell.ProductId,
                    productSellId = item.ProductSellId,
                    shopTitle = shop.Title,
                    slug = productSell.Product.Slug,
                    title = productSell.Product.Title,
                    unit = item.Unit
                });
            }
        }
        return model;
    }

    public OrderDetailForUserPanelQueryModel GetOrderDetailForUserPanel(int id, int userId)
    {
        var order = _shopContext.Orders.Include(o=>o.OrderSellers).ThenInclude(s=>s.OrderItems)
            .SingleOrDefault(o=>o.Id == id && o.UserId == userId);
        if (order == null) return null;
        OrderDetailForUserPanelQueryModel model = new()
        {
            OrderAddress = new(),
            PriceAfterOff = order.PriceAfterOff,
            DiscountPercent = order.DiscountPercent,
            DiscountTitle = order.DiscountTitle,
            Id = order.Id,
            OrderPayment = order.OrderPayment,
            OrderSellers = order.OrderSellers.Select(s=> new OrderSellerDetailForUserPanelQueryModel
            {
                PriceAfterOff= s.PriceAfterOff,
                SellerAddress = "",
                DiscountId = s.DiscountId,
                DiscountPercent= s.DiscountPercent,
                DiscountTitle= s.DiscountTitle,
                Id= s.Id,
                DiscountPrice = s.DiscountPercent * s.PriceAfterOff / 100,
                OrderId = s.OrderId,
                OrderItems = s.OrderItems.Select(i=> new OrderItemDetailForUserPanelQueryModel
                {
                    PriceAfterOff = i.PriceAfterOff,
                    SumPriceAfterOff= i.SumPriceAfterOff,
                    Count = i.Count,
                    Id= i.Id,
                    OrderSellerId = i.OrderSellerId,
                    Price = i.Price,
                    ProductId = 0,
                    ProductImageName = "",
                    ProductSellId = i.ProductSellId,
                    ProductTitle = "",
                    SumPrice = i.SumPrice,
                    Unit = i.Unit
                }).ToList(),
                PaymentPrice = s.PaymentPrice,
                PostId = s.PostId,
                PostPrice = s.PostPrice,
                PostTitle = s.PostTitle,
                Price = s.Price,
                SellerId = s.SellerId,
                Status = s.Status,
            }).ToList(),
            OrderStatus = order.OrderStatus,
            PaymentPrice = order.PaymentPrice,
            PaymentPriceSeller = order.PaymentPriceSeller,
            PostPrice = order.PostPrice,
            Price = order.Price,    
            UpdateDate = order.UpdateDate.ToPersainDate()
        };
        var address = _shopContext.OrderAddresses.Single(o => o.Id == order.OrderAddressId);
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
        foreach(var seller in model.OrderSellers)
        {
            var s = _shopContext.Sellers.Find(seller.SellerId);
            var c = _post_Context.Cities.Include(c => c.State).Single(c => c.Id == s.CityId && c.StateId == s.StateId);
            seller.SellerAddress = $"{s.Title} - {c.State.Title} - {c.Title} - {s.Address}";
            foreach(var item in seller.OrderItems)
            {
                var productSell = _shopContext.ProductSells.Include(p => p.Product).Single(s => s.Id == item.ProductSellId);
                item.ProductId = productSell.ProductId;
                item.ProductTitle = productSell.Product.Title;
                item.ProductImageName = $"{FileDirectories.ProductImageDirectory100}{productSell.Product.ImageName}";
            }
        }
        return model;   
    }
    public OrderDetailForUserPanelQueryModel GetOrderDetailForUserPanel(int id)
    {
        var order = _shopContext.Orders.Include(o => o.OrderSellers).ThenInclude(s => s.OrderItems)
            .SingleOrDefault(o => o.Id == id);
        if (order == null || order.OrderStatus != Shared.Domain.Enum.OrderStatus.پرداخت_شده) return null;
        OrderDetailForUserPanelQueryModel model = new()
        {
            OrderAddress = new(),
            PriceAfterOff = order.PriceAfterOff,
            DiscountPercent = order.DiscountPercent,
            DiscountTitle = order.DiscountTitle,
            Id = order.Id,
            OrderPayment = order.OrderPayment,
            OrderSellers = order.OrderSellers.Select(s => new OrderSellerDetailForUserPanelQueryModel
            {
                PriceAfterOff = s.PriceAfterOff,
                SellerAddress = "",
                DiscountId = s.DiscountId,
                DiscountPercent = s.DiscountPercent,
                DiscountTitle = s.DiscountTitle,
                Id = s.Id,
                DiscountPrice = s.DiscountPercent * s.PriceAfterOff / 100,
                OrderId = s.OrderId,
                OrderItems = s.OrderItems.Select(i => new OrderItemDetailForUserPanelQueryModel
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
                PaymentPrice = s.PaymentPrice,
                PostId = s.PostId,
                PostPrice = s.PostPrice,
                PostTitle = s.PostTitle,
                Price = s.Price,
                SellerId = s.SellerId,
                Status = s.Status,
            }).ToList(),
            OrderStatus = order.OrderStatus,
            PaymentPrice = order.PaymentPrice,
            PaymentPriceSeller = order.PaymentPriceSeller,
            PostPrice = order.PostPrice,
            Price = order.Price,
            UpdateDate = order.UpdateDate.ToPersainDate()
        };
        var address = _shopContext.OrderAddresses.Single(o => o.Id == order.OrderAddressId);
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
        foreach (var seller in model.OrderSellers)
        {
            var s = _shopContext.Sellers.Find(seller.SellerId);
            var c = _post_Context.Cities.Include(c => c.State).Single(c => c.Id == s.CityId && c.StateId == s.StateId);
            seller.SellerAddress = $"{s.Title} - {c.State.Title} - {c.Title} - {s.Address}";
            foreach (var item in seller.OrderItems)
            {
                var productSell = _shopContext.ProductSells.Include(p => p.Product).Single(s => s.Id == item.ProductSellId);
                item.ProductId = productSell.ProductId;
                item.ProductTitle = productSell.Product.Title;
                item.ProductImageName = $"{FileDirectories.ProductImageDirectory100}{productSell.Product.ImageName}";
            }
        }
        return model;
    }
    public OrderUserPanelPaging GetOrdersForUserPanel(int userId, int pageId, int take)
    {
        var res = _shopContext.Orders.Include(o=>o.OrderSellers).ThenInclude(s=>s.OrderItems).Where(o => o.UserId == userId).OrderByDescending(o=>o.Id);
        OrderUserPanelPaging model = new();
        model.GetData(res, pageId, take,2);
        model.Orders = new();
        if (res.Count() > 0)
            model.Orders = res.Skip(model.Skip).Take(model.Take)
                .Select(o => new OrderForUserPanelQueryModel
                {
                    PriceAfterOff = o.PriceAfterOff,
                    CreationDate = o.CreateDate.ToPersainDate(),
                    DiscountPercent = o.DiscountPercent,
                    DiscountTitle = o.DiscountTitle,
                    Id = o.Id,
                    OrderStatus = o.OrderStatus,
                    PaymentPrice = o.PaymentPrice,
                    PaymentPriceSeller = o.PaymentPriceSeller,
                    PostPrice = o.PostPrice,
                    Price = o.Price,
                    UpdateDate = o.UpdateDate.ToPersainDate()
                }).ToList();
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

    public async Task<bool> IsOpenOrderSellerForUser(int id, int userId)
    {
        var seller = await _shopContext.OrderSellers.Include(s=>s.Order)
            .SingleOrDefaultAsync(s=>
            s.Id == id && s.Order.UserId == userId && s.Order.OrderStatus == Shared.Domain.Enum.OrderStatus.پرداخت_نشده);
        return seller != null;
    }
}

using Discounts.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PostModule.Infrastracture.EF;
using Query.Contract.Admin.Order;
using Query.Contract.UserPanel.Order;
using Shared.Application;
using Shared.Domain.Enum;
using Shop.Infrastructure;
using Users.Domain.UserAgg;
using Users.Infrastructure;

namespace Query.Services.Admin;
internal class OrderAdminQuery : IOrderAdminQuery
{
    private readonly ShopContext _shopContext;
    private readonly DiscountContext _discountContext;
    private readonly Post_Context _post_Context;
    private readonly UserContext _userContext;

    public OrderAdminQuery(ShopContext shopContext, DiscountContext discountContext, Post_Context post_Context, UserContext userContext)
    {
        _shopContext = shopContext;
        _discountContext = discountContext;
        _post_Context = post_Context;
        _userContext = userContext;
    }

    public OrderDetailForAdminQueryModel GetOrderDetailForAdmin(int id)
    {
        var order = _shopContext.Orders.Include(o => o.OrderSellers).ThenInclude(s => s.OrderItems)
            .SingleOrDefault(o => o.Id == id);
        if (order == null) return null;
        OrderDetailForAdminQueryModel model = new()
        {
            User = new(),
            OrderAddress = null,
            PriceAfterOff = order.PriceAfterOff,
            DiscountPercent = order.DiscountPercent,
            DiscountTitle = order.DiscountTitle,
            Id = order.Id,
            OrderPayment = order.OrderPayment,
            OrderSellers = order.OrderSellers.Select(s => new OrderSellerDetailForAdminQueryModel
            {
                PriceAfterOff = s.PriceAfterOff,
                SellerAddress = "",
                DiscountId = s.DiscountId,
                DiscountPercent = s.DiscountPercent,
                DiscountTitle = s.DiscountTitle,
                Id = s.Id,
                DiscountPrice = s.DiscountPercent * s.PriceAfterOff / 100,
                OrderId = s.OrderId,
                OrderItems = s.OrderItems.Select(i => new OrderItemDetailForAdminQueryModel
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
        if(order.OrderAddressId > 0)
        {
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
        }
        foreach (var orderSeller in model.OrderSellers)
        {
            var seller = _shopContext.Sellers.Find(orderSeller.SellerId);
            var city = _post_Context.Cities.Include(c => c.State).Single(c => c.Id == seller.CityId && c.StateId == seller.StateId);
            orderSeller.SellerAddress = $"{seller.Title} - {city.State.Title} - {city.Title} - {seller.Address}";
            foreach (var item in orderSeller.OrderItems)
            {
                var productSell = _shopContext.ProductSells.Include(p => p.Product).Single(s => s.Id == item.ProductSellId);
                item.ProductId = productSell.ProductId;
                item.ProductTitle = productSell.Product.Title;
                item.ProductImageName = $"{FileDirectories.ProductImageDirectory100}{productSell.Product.ImageName}";
            }
        }
        var user = _userContext.Users.Find(order.UserId);
        model.User = new()
        {
            Email = user.Email,
            FullName = user.FullName,
            Mobile = user.Mobile,
            UserId = user.Id
        };
        return model;
    }

    public OrderAdminPaging GetOrdersForAdmin(int pageId, int take, int OrderId,int userId,OrderAdminStatus Status)
    {
        string title = "لیست همه فاکتور ها";
        User user = null;
        if(userId > 0)
            user = _userContext.Users.SingleOrDefault(u=>u.Id == userId);   
        var res = _shopContext.Orders.Include(o => o.OrderSellers).ThenInclude(s => s.OrderItems).OrderByDescending(o => o.Id);
        if(OrderId > 0) 
            res = res.Where(r=>r.Id == OrderId).OrderByDescending(o => o.Id);
        switch (Status)
        {
            case OrderAdminStatus.همه:
                break;
            case OrderAdminStatus.پرداخت_نشده:
                res = res.Where(r => r.OrderStatus == OrderStatus.پرداخت_نشده).OrderByDescending(o => o.Id);
                title = "لیست فاکتور های پرداخت نشده";
                break;
            case OrderAdminStatus.پرداخت_شده:
                res = res.Where(r => r.OrderStatus == OrderStatus.پرداخت_شده).OrderByDescending(o => o.Id);
                title = "لیست فاکتور های پرداخت شده";
                break;
            case OrderAdminStatus.لغو_شده_توسط_مشتری:
                res = res.Where(r => r.OrderStatus == OrderStatus.لغو_شده_توسط_مشتری).OrderByDescending(o => o.Id);
                title = "لیست فاکتور های لغو شده توسط مشتری";
                break;
            case OrderAdminStatus.لغو_شده_توسط_ادمین:
                res = res.Where(r => r.OrderStatus == OrderStatus.لغو_شده_توسط_ادمین).OrderByDescending(o => o.Id);
                title = "لیست فاکتور های لغو شده توسط ادمین";
                break;
            case OrderAdminStatus.ارسال_شده:
                res = res.Where(r => r.OrderStatus == OrderStatus.ارسال_شده).OrderByDescending(o => o.Id);
                title = "لیست فاکتور های ارسال شده ";
                break;
            default:
                break;
        }
        if(userId > 0)
        {
            res = res.Where(r => r.UserId == userId).OrderByDescending(o => o.Id);
            if (user != null)
            {
                var userName = string.IsNullOrEmpty(user.FullName) ? user.Mobile : user.FullName;
                title = title + $"برای کاربر : {userName}";
            }
        }
        OrderAdminPaging model = new();
        model.GetData(res, pageId, take, 2);
        model.Status = Status;
        model.OrderId = OrderId;
        model.PageTitle = title;
        model.UserId = userId;
        model.Orders = new();
        if(res.Count() > 0)
        {
            model.Orders = res.Skip(model.Skip).Take(model.Take)
              .Select(o => new OrderForAdminQueryModel
              {
                  DiscountId = o.DiscountId,
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
                  UpdateDate = o.UpdateDate.ToPersainDate(),
                  UserId = o.UserId,
                  UserName = "",
                  BackgroundColor = "orange"
              }).ToList();

            model.Orders.ForEach(x =>
            {
                var user = _userContext.Users.Find(x.UserId);
                x.UserName = user.Mobile;
                switch (x.OrderStatus)
                {
                    case OrderStatus.پرداخت_نشده:
                        break;
                    case OrderStatus.پرداخت_شده:
                        x.BackgroundColor = "green";
                        break;
                    case OrderStatus.لغو_شده_توسط_مشتری:
                        x.BackgroundColor = "red";
                        break;
                    case OrderStatus.لغو_شده_توسط_ادمین:
                        x.BackgroundColor = "red";
                        break;
                    case OrderStatus.ارسال_شده:
                        x.BackgroundColor = "aqua";
                        break;
                    default:
                        break;
                }
            });
        }
        return model;
    }
}

using Microsoft.EntityFrameworkCore.Storage;
using PostModule.Domain.Services;
using Query.Contract.UserPanel.Seller;
using Shared.Application;
using Shop.Domain.ProductSellAgg;
using Shop.Domain.SellerAgg;
using Shared.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Discounts.Infrastructure;

namespace Query.Services.UserPanel;
internal class SellerUserPanelQuery : ISellerUserPanelQuery
{
    private readonly ISellerRepository _sellerRepository;
    private readonly IStateRepository _stateRepository; 
    private readonly ICityRepository _cityRepository;
    private readonly IProductSellRepository _productSellRepository;
    private readonly ShopContext _shopContext;
    private readonly DiscountContext _discountContext;
    public SellerUserPanelQuery(ISellerRepository sellerRepository, IStateRepository stateRepository, 
        ICityRepository cityRepository, IProductSellRepository productSellRepository, ShopContext shopContext,
        DiscountContext discountContext)
    {
        _sellerRepository = sellerRepository;
        _stateRepository = stateRepository;
        _cityRepository = cityRepository;
        _productSellRepository = productSellRepository; 
        _shopContext = shopContext; 
        _discountContext = discountContext;
    }

    public SellerDetailForUserPanelQueryModel GetSellerDetailForSeller(int id, int userId)
    {
        var seller = _sellerRepository.GetSellerForUserPanel(id, userId);
        if (seller == null) return null;
        SellerDetailForUserPanelQueryModel model = new()
        {
            Address = seller.Address,
            ImageAccept = seller.ImageAccept,
            WhatsApp = seller.WhatsApp,
            ImageAlt = seller.ImageAlt,
            CityId = seller.CityId,
            CreationDate = seller.CreateDate.ToPersainDate(),
            Email = seller.Email,
            GoogleMapUrl = seller.GoogleMapUrl,
            Id = id,
            ImageName = seller.ImageName,
            Instagram = seller.Instagram,   
            Phone1 = seller.Phone1,
            Phone2 = seller.Phone2,
            StateId = seller.StateId,
            Status = seller.Status,
            Telegram = seller.Telegram,
            Title = seller.Title,
            UpdateDate = seller.UpdateDate.ToPersainDate(),
            CityName = ""
        };
        var state = _stateRepository.GetById(seller.StateId);
        var city = _cityRepository.GetById(seller.CityId);
        model.CityName = $"{state.Title} - {city.Title}";
        return model;
    }

    public SellerProductPageUserPanelQueryModel GetSellerProductsForUserPanel(int pageId, string filter, int sellerId, int userId)
    {
        var seller = _sellerRepository.GetSellerForUserPanel(sellerId, userId);
        if (seller == null) return null;
        var res = _shopContext.ProductSells.Include(s => s.OrderItems)
            .ThenInclude(o => o.OrderSeller).Include(s => s.Product).Where(s => s.SellerId == sellerId);
        if(!string.IsNullOrEmpty(filter))
            res = res.Where(r=>r.Product.Title.Contains(filter));
        SellerProductPageUserPanelQueryModel model = new();
        model.GetData(res, pageId, 15, 2);
        model.Filter = filter;
        model.SellerId = sellerId;
        model.SellerTitle = seller.Title;
        model.Products = new();
        if(res.Count() > 0)
        {
            model.Products = res.Skip(model.Skip).Take(model.Take).
                Select(r => new ProductSellUserPanelQueryModel
                {
                    Active = r.Active,
                    Amount = r.Amount,  
                    Id = r.Id,
                    Price = r.Price,
                    ProductId = r.ProductId,
                    ProductImageName = r.Product.ImageName,
                    ProductSlug = r.Product.Slug,
                    ProductTitle = r.Product.Title,
                    SellCount = r.OrderItems.Where(i=>i.OrderSeller.Status == OrderSellerStatus.پرداخت_شده || 
                    i.OrderSeller.Status == OrderSellerStatus.در_حال_آماده_سازی || 
                    i.OrderSeller.Status == OrderSellerStatus.ارسال_شده).Sum(o=>o.Count),
                    SellerId = r.SellerId,
                    Unit = r.Unit,
                    Weight = r.Weight,
                    ProductDiscountPercent = 0,
                    ProductSellDiscountPercent = 0,
                    PriceAfterDiscount = r.Price
                }).ToList();
            model.Products.ForEach(x =>
            {
                var productDiscounts = _discountContext.ProductDiscounts.Where(p => (p.ProductId == x.ProductId || p.ProductSellId == x.Id)
                && (p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date));
                if(productDiscounts.Any(p=>p.ProductId == x.ProductId && p.ProductSellId == 0))
                {
                    var dis = productDiscounts.OrderBy(p => p.Id).Last(p => p.ProductId == x.ProductId && p.ProductSellId == 0);
                    x.ProductDiscountPercent = dis.Percent;
                    x.PriceAfterDiscount = x.PriceAfterDiscount - (dis.Percent * x.Price / 100);
                }
                if (productDiscounts.Any(p => p.ProductId == x.ProductId && p.ProductSellId == x.Id))
                {
                    var dis = productDiscounts.OrderBy(p => p.Id).Last(p => p.ProductId == x.ProductId && p.ProductSellId == x.Id);
                    x.ProductSellDiscountPercent = dis.Percent;
                    x.PriceAfterDiscount = x.PriceAfterDiscount - (dis.Percent * x.Price / 100);
                }
            });
        }
        return model;
    }

    public List<SellersUserPanelQueryModel> GetSellersForUser(int userId)
    {
        var res = _sellerRepository.GetAllByQuery(c => c.UserId == userId);
        List<SellersUserPanelQueryModel> model =  res.Select(r => new SellersUserPanelQueryModel
        {
            ImageAccept = r.ImageAccept,
            CityId = r.CityId,
            CityName = "",
            CreationDate = r.CreateDate.ToPersainDate(),
            Id = r.Id,
            ImageName = r.ImageName,
            Phone1 = r.Phone1,
            StateId = r.StateId,
            Status = r.Status,
            Title = r.Title
        }).ToList();
        model.ForEach(x =>
        {
            var state = _stateRepository.GetById(x.StateId);
            var city = _cityRepository.GetById(x.CityId);
            x.CityName = $"{state.Title} - {city.Title}";
        });
        return model;
    }

    public List<SellersForAddDiscountUserPanelQueryModel> GetSellersForUserForAddDiscount(int userId)
    {
        var res = _sellerRepository.GetAllByQuery(c => c.UserId == userId);
        List<SellersForAddDiscountUserPanelQueryModel> model = res.Select(r => new SellersForAddDiscountUserPanelQueryModel
        {
            Id = r.Id,
            Title = r.Title
        }).ToList();
        return model;
    }

    public List<int> GetUserSellerIds(int userId)
    {
        return _shopContext.Sellers.Where(s=>s.UserId == userId).Select(s => s.Id).ToList();    
    }

    public async Task<bool> IsProductSellForUser(int userId, int id)
    {
        return await _shopContext.ProductSells.Include(p=>p.Seller)
            .AnyAsync(c=>c.Seller.UserId == userId && c.Id == id);
    }

    public bool IsSellerForUser(int id, int userId)
    {
       var seller = _sellerRepository.GetById(id);
        return seller.UserId == userId; 
    }
}

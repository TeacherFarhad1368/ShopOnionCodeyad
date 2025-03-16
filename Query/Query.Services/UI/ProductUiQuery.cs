using Blogs.Domain.BlogCategoryAgg;
using Discounts.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PostModule.Infrastracture.EF;
using Query.Contract.UI;
using Query.Contract.UI.Product;
using Seos.Domain;
using Seos.Infrastructure;
using Shared.Application;
using Shared.Domain.Enum;
using Shop.Infrastructure;
using System.Linq;
using Users.Domain.UserAgg;

namespace Query.Services.UI;
internal class ProductUiQuery : IProductUiQuery
{
    private readonly ShopContext _shopContext;
    private readonly ISeoRepository _seoRepository;
    private readonly Post_Context _postContext;
    private readonly DiscountContext _discountContext;
    public ProductUiQuery(ShopContext shopContext, ISeoRepository seoRepository, Post_Context post_Context,
        DiscountContext discountContext)
    {
        _shopContext = shopContext;
        _seoRepository = seoRepository; 
        _postContext = post_Context;
        _discountContext = discountContext;
    }
    private List<BreadCrumbQueryModel> GetProductBreadCrumb(string? categorySlug,string? productSlug)
    {
        if(!string.IsNullOrWhiteSpace(categorySlug))
        {
            List<BreadCrumbQueryModel> model = new()
            {
                 new BreadCrumbQueryModel(){Number = 1 , Title ="خانه" , Url = "/"} ,
                 new BreadCrumbQueryModel(){Number = 2, Title = "محصولات" , Url = "/Shop"}
            };
            var category = _shopContext.ProductCategories.SingleOrDefault(c=>c.Slug.Trim().ToLower()==categorySlug.ToLower().Trim());  
            if(category != null)
            {
                int i = 3;
                if(category.Parent > 0)
                {
                    var parent = _shopContext.ProductCategories.Find(category.Parent);
                    model.Add(new BreadCrumbQueryModel
                    {
                        Number = i,
                        Title = parent.Title,
                        Url = $"/Shop?slug={parent.Slug}"
                    });
                    i++;
                }
                model.Add(new BreadCrumbQueryModel
                {
                    Number = i,
                    Title = category.Title,
                    Url = ""
                });
            }
            return model;
        }
        else if (!string.IsNullOrWhiteSpace(productSlug))
        {
            List<BreadCrumbQueryModel> model = new()
            {
                 new BreadCrumbQueryModel(){Number = 1 , Title ="خانه" , Url = "/"} ,
                 new BreadCrumbQueryModel(){Number = 2, Title = "محصولات" , Url = "/Shop"}
            };
            var product = _shopContext.Products.Include(p=>p.ProductCategoryRelations).SingleOrDefault(p=>p.Slug.Trim().ToLower() ==  productSlug.ToLower().Trim());
            if (product == null) return model;
            int i = 3;
            if (product.ProductCategoryRelations.Any())
            {
                var categoryId = product.ProductCategoryRelations.First().ProductCategoryId;
                var category = _shopContext.ProductCategories.SingleOrDefault(c => c.Id == categoryId);
                if (category != null)
                {
                    if (category.Parent > 0)
                    {
                        var parent = _shopContext.ProductCategories.Find(category.Parent);
                        model.Add(new BreadCrumbQueryModel
                        {
                            Number = i,
                            Title = parent.Title,
                            Url = $"/Shop?slug={parent.Slug}"
                        });
                        i++;
                    }
                    model.Add(new BreadCrumbQueryModel
                    {
                        Number = i,
                        Title = category.Title,
                        Url = $"/Shop?slug={category.Slug}"
                    });
                    i++;
                }
            }
            model.Add(new BreadCrumbQueryModel
            {
                Number = i,
                Title = product.Title,
                Url = ""
            });
            return model;
        }
        else
        {
            return new()
        {
            new BreadCrumbQueryModel(){Number = 1 , Title ="خانه" , Url = "/"} ,
            new BreadCrumbQueryModel(){Number = 2, Title = "محصولات" , Url = "/Shop"}
        };
        }
    }
    public ShopPaging GetProductsForUi(int pageId, string filter, string categorySlug, int Id, ShopOrderBy orderBy)
    {
        int ownerSeoId = 0;
        string shopTitle = "محصولات "; 
        string seoTitle = "محصولات ";
        var res = _shopContext.Products.Include(p => p.ProductSells).ThenInclude(s=>s.OrderItems)
            .Include(p => p.ProductSells).ThenInclude(s => s.Seller)
            .Include(p => p.ProductCategoryRelations).ThenInclude(c => c.ProductCategory)
            .Where(p => p.Active && p.ProductSells.Count() > 0).OrderBy(p=>p.Id);
        if (Id > 0)
        {
            var seller = _shopContext.Sellers.SingleOrDefault(s => s.Id == Id);
            if(seller != null)
            {
                res = res.Where(r=>r.ProductSells.Any(s=>s.SellerId == Id)).OrderBy(p => p.Id);
                shopTitle = shopTitle + " فروشگاه " + seller.Title;
            }
        }
        if (!string.IsNullOrEmpty(categorySlug))
        {
            var category = _shopContext.ProductCategories.SingleOrDefault(c => c.Slug == categorySlug);
            if(category != null)
            {
                res = res.Where(r => r.ProductCategoryRelations.Any(s => s.ProductCategoryId == category.Id)).OrderBy(p => p.Id);
                shopTitle = shopTitle + "دسته بندی " + category.Title;
                seoTitle = "دسته بندی " + category.Title;
                ownerSeoId = category.Id;
            }
        }
        if(!string.IsNullOrEmpty(filter))
            res = res.Where(r => r.Title.ToLower().Contains(filter.ToLower()) || r.Description.ToLower().Contains(filter.ToLower())).OrderBy(p => p.Id);
        switch (orderBy)
        {
            case ShopOrderBy.جدید_ترین:
                res = res.OrderByDescending(p => p.Id);
                break;
            case ShopOrderBy.قدیمی_ترین:
                res = res.OrderBy(p => p.Id);
                break;
            case ShopOrderBy.پرفروش_ترین:
                res = res.OrderByDescending(p => p.ProductSells.OrderByDescending(b=>b.OrderItems.Count).First().OrderItems.Count);
                break;
            case ShopOrderBy.ارزان_ترین:
                res = res.OrderBy(p => p.ProductSells.First().Price);
                break;
            case ShopOrderBy.گران_ترین:
                res = res.OrderByDescending(p => p.ProductSells.First().Price);
                break;
        }

        ShopPaging model = new();
        model.GetData(res, pageId, 6, 2);
        model.Filter = filter;
        model.ShopId = Id;
        model.ShopTitle = shopTitle;
        model.CategorySlug = categorySlug;
        model.OrderBy = orderBy;
        model.Categories = new();
        model.BreadCrumb = new();
        model.Products = new List<ProductShopUiQueryModel>();
        if(res.Count() > 0)
        {
            model.Products = res.Skip(model.Skip).Take(model.Take)
                .Select(p => new ProductShopUiQueryModel
                {
                    ImageAlt = p.ImageAlt,
                    PriceAfterOff = p.ProductSells.OrderByDescending(b => b.OrderItems.Count).First().Price,
                    Id = p.Id,
                    ImageName = FileDirectories.ProductImageDirectory500 + p.ImageName,
                    Price = p.ProductSells.OrderByDescending(b => b.OrderItems.Count).First().Price,
                    Shop = p.ProductSells.OrderByDescending(b=>b.OrderItems.Count).First().Seller.Title,
                    Slug = p.Slug,
                    Title = p.Title
                }).ToList();
            model.Products.ForEach(x =>
            {
                if(_discountContext.ProductDiscounts.Any(p=>p.ProductId == x.Id && p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date))
                {
                    var discount = _discountContext.ProductDiscounts.OrderByDescending(p => p.Percent)
                    .First(p => p.ProductId == x.Id && p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date);
                    if(discount.ProductSellId > 0)
                    {
                        var sellProduct = _shopContext.ProductSells.Find(discount.ProductSellId);
                        x.Shop = _shopContext.Sellers.Find(sellProduct.SellerId).Title;
                        x.Price = sellProduct.Price;
                        x.PriceAfterOff = sellProduct.Price - (discount.Percent * sellProduct.Price / 100);
                    }
                    else
                    {
                        x.PriceAfterOff = x.Price - (discount.Percent * x.Price / 100);
                    }
                }
            });
        }
        var cat = _shopContext.ProductCategories.Where(c => c.Active);
        model.Categories = cat.Where(c=>c.Parent == 0).Select(c => new ProductCategoryUiQueryModel
        {
            Title = c.Title,
            Slug = c.Slug,
            Childs = cat.Where(d=>d.Parent == c.Id).Select(d=> new ProductCategoryUiQueryModel
            {
                Childs = null,
                Slug = d.Slug,
                Title=d.Title
            }).ToList()
        }).ToList();
        model.BreadCrumb = GetProductBreadCrumb(model.CategorySlug, "");
        var seo = _seoRepository.GetSeoForUi(ownerSeoId, WhereSeo.ProductCategory, seoTitle);
        model.Seo = new(seo.MetaTitle, seo.MetaDescription, seo.MetaKeyWords, seo.IndexPage, seo.Canonical, seo.Schema);
        return model;
    }
    public SingleProductUIQueryModel GetSingleProductForUi(int id)
    {
        var product = _shopContext.Products.Include(p=>p.ProductCategoryRelations).ThenInclude(p=>p.ProductCategory)
            .Include(p=>p.ProductFeatures)
            .Include(p=>p.ProductGalleries)
            .Include(p=>p.ProductSells.Where(s=>s.Active))
            .SingleOrDefault(c=>c.Id == id);
        if (product == null) return null;
        SingleProductUIQueryModel model = new()
        {
            ImageAlt = product.ImageAlt,
            BreadCrumb = new(),
            Categories = product.ProductCategoryRelations.Select(r=> new CategoryForProductSingleQueryModel
            {
                Slug = r.ProductCategory.Slug,
                Title = r.ProductCategory.Title,
            }).ToList(),
            Description = product.Description,
            Features = product.ProductFeatures.Select(f=> new FeatureForProductSingleQueryModel
            {
                Title = f.Title,
                Value = f.Value,
            }).ToList(),
            Galleries = product.ProductGalleries.Select(g=> new GalleryForProductSingleQueryModel
            {
                ImageAlt= g.ImageAlt,
                ImageName= g.ImageName,
            }).ToList(),
            Id = product.Id,
            ImageName = product.ImageName,
            ProductSells = product.ProductSells.Select(s=> new ProductSellForProductSingleQueryModel
            {
                Amount = s.Amount,
                SellerAddress = "",
                Price = s.Price,
                PriceAfterOff = s.Price,
                SellerId = s.SellerId,
                SellerName = "",
                ProductId = s.ProductId,
                Unit = s.Unit,
                Weight = s.Weight,
                Id = s.Id,
            }).ToList(),
            Seo = null,
            Slug = product.Slug,
            Title = product.Title,
            Weight= product.Weight
        };
        model.BreadCrumb = GetProductBreadCrumb(null, product.Slug);
        var seo = _seoRepository.GetSeoForUi(product.Id, WhereSeo.Product, product.Title);
        model.Seo = new(seo.MetaTitle, seo.MetaDescription, seo.MetaKeyWords, seo.IndexPage, seo.Canonical, seo.Schema);
        model.ProductSells.ForEach(x =>
        {
            var seller = _shopContext.Sellers.Find(x.SellerId);
            x.SellerName = seller.Title;
            var city = _postContext.Cities.Include(c => c.State).SingleOrDefault(c => c.Id == seller.CityId && c.StateId == seller.StateId);
            if (city != null)
                x.SellerAddress = $"{city.State.Title} {city.Title}";

            var discount = _discountContext.ProductDiscounts.FirstOrDefault(d => d.ProductSellId == x.Id && d.StartDate.Date <= DateTime.Now.Date && d.EndDate.Date >= DateTime.Now.Date);
            if(discount  != null)
            {
                x.PriceAfterOff = x.Price - (discount.Percent * x.Price / 100);
            }
            else
            {
                var productDiscount = _discountContext.ProductDiscounts.FirstOrDefault(d => d.ProductId == model.Id && d.ProductSellId == 0 && d.StartDate.Date <= DateTime.Now.Date && d.EndDate.Date >= DateTime.Now.Date);
                if(productDiscount != null)
                {
                    x.PriceAfterOff = x.Price - (productDiscount.Percent * x.Price / 100);
                }

            }
        });
        return model;
    }
    public List<ProductCartForIndexQueryModel> GetBestPeoductSellForIndex()
    {
        var model = _shopContext.Products
    .Include(p => p.ProductSells).ThenInclude(s => s.OrderItems)
    .Include(p => p.ProductSells).ThenInclude(s => s.Seller)
    .Where(p => p.Active && p.ProductSells.Any()) 
    .AsEnumerable() 
    .OrderByDescending(p => p.ProductSells.Sum(s => s.OrderItems.Count))
    .Take(10).Select(s => new ProductCartForIndexQueryModel
      {
        Id = s.Id,
        Title = s.Title,
        Amount = s.ProductSells.Sum(s=>s.Amount),
        ImageAlt = s.ImageAlt,
        PriceAfterOff = s.ProductSells.First().Price,
        ImageName =FileDirectories.ProductImageDirectory500 + s.ImageName,
        Price = s.ProductSells.First().Price,
        Shop = s.ProductSells.First().Seller.Title,
        Slug = s.Slug,
        isWishList = false
       }).ToList();
        if(model.Count > 0)
        {
            model.ForEach(x =>
            {
                if (_discountContext.ProductDiscounts.Any(p => p.ProductId == x.Id && p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date))
                {
                    var discount = _discountContext.ProductDiscounts.OrderByDescending(p => p.Percent)
                    .First(p => p.ProductId == x.Id && p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date);
                    if (discount.ProductSellId > 0)
                    {
                        var sellProduct = _shopContext.ProductSells.Find(discount.ProductSellId);
                        x.Shop = _shopContext.Sellers.Find(sellProduct.SellerId).Title;
                        x.Price = sellProduct.Price;
                        x.PriceAfterOff = sellProduct.Price - (discount.Percent * sellProduct.Price / 100);
                    }
                    else
                    {
                        x.PriceAfterOff = x.Price - (discount.Percent * x.Price / 100);
                    }
                }
            });
        }
        return model;   
    }
    public List<ProductCartForIndexQueryModel> GetBestPeoductVisitForIndex()
    {
        var model = _shopContext.Products
    .Include(p => p.ProductVisits)
    .Include(p => p.ProductSells).ThenInclude(s => s.Seller)
    .Where(p => p.Active && p.ProductSells.Any())
    .AsEnumerable()
    .OrderByDescending(p => p.ProductVisits.Sum(v=>v.Count))
    .Take(10).Select(s => new ProductCartForIndexQueryModel
    {
        Id = s.Id,
        Title = s.Title,
        Amount = s.ProductSells.Sum(s => s.Amount),
        ImageAlt = s.ImageAlt,
        PriceAfterOff = s.ProductSells.First().Price,
        ImageName = FileDirectories.ProductImageDirectory500 + s.ImageName,
        Price = s.ProductSells.First().Price,
        Shop = s.ProductSells.First().Seller.Title,
        Slug = s.Slug,
        isWishList = false
    }).ToList();
        if (model.Count > 0)
        {
            model.ForEach(x =>
            {
                if (_discountContext.ProductDiscounts.Any(p => p.ProductId == x.Id && p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date))
                {
                    var discount = _discountContext.ProductDiscounts.OrderByDescending(p => p.Percent)
                    .First(p => p.ProductId == x.Id && p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date);
                    if (discount.ProductSellId > 0)
                    {
                        var sellProduct = _shopContext.ProductSells.Find(discount.ProductSellId);
                        x.Shop = _shopContext.Sellers.Find(sellProduct.SellerId).Title;
                        x.Price = sellProduct.Price;
                        x.PriceAfterOff = sellProduct.Price - (discount.Percent * sellProduct.Price / 100);
                    }
                    else
                    {
                        x.PriceAfterOff = x.Price - (discount.Percent * x.Price / 100);
                    }
                }
            });
        }
        return model;
    }
    public List<ProductCartForIndexQueryModel> GetNewPeoductForIndex()
    {
        var model = _shopContext.Products
    .Include(p => p.ProductSells).ThenInclude(s => s.Seller)
    .Where(p => p.Active && p.ProductSells.Any())
    .AsEnumerable()
    .OrderByDescending(p => p.Id)
    .Take(10).Select(s => new ProductCartForIndexQueryModel
    {
        Id = s.Id,
        Title = s.Title,
        Amount = s.ProductSells.Sum(s => s.Amount),
        ImageAlt = s.ImageAlt,
        PriceAfterOff = s.ProductSells.First().Price,
        ImageName = FileDirectories.ProductImageDirectory500 + s.ImageName,
        Price = s.ProductSells.First().Price,
        Shop = s.ProductSells.First().Seller.Title,
        Slug = s.Slug,
        isWishList = false
    }).ToList();
        if (model.Count > 0)
        {
            model.ForEach(x =>
            {
                if (_discountContext.ProductDiscounts.Any(p => p.ProductId == x.Id && p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date))
                {
                    var discount = _discountContext.ProductDiscounts.OrderByDescending(p => p.Percent)
                    .First(p => p.ProductId == x.Id && p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date);
                    if (discount.ProductSellId > 0)
                    {
                        var sellProduct = _shopContext.ProductSells.Find(discount.ProductSellId);
                        x.Shop = _shopContext.Sellers.Find(sellProduct.SellerId).Title;
                        x.Price = sellProduct.Price;
                        x.PriceAfterOff = sellProduct.Price - (discount.Percent * sellProduct.Price / 100);
                    }
                    else
                    {
                        x.PriceAfterOff = x.Price - (discount.Percent * x.Price / 100);
                    }
                }
            });
        }
        return model;
    }
    public List<WishListProductQueryModel> GetWishListForUserLoggedIn(int userId)
    {
        var model = _shopContext.Products
            .Include(c=>c.WishLists)
    .Include(p => p.ProductSells).ThenInclude(s => s.OrderItems)
    .Include(p => p.ProductSells).ThenInclude(s => s.Seller)
    .Where(p => p.Active && p.WishLists.Any(w=>w.UserId == userId))
    .AsEnumerable()
    .OrderByDescending(p => p.ProductSells.Sum(s => s.OrderItems.Count))
    .Take(10).Select(s => new WishListProductQueryModel
    {
        ProductId = s.Id,
        Title = s.Title,
        Amount = s.ProductSells.Sum(s => s.Amount),
        ImageAlt = s.ImageAlt,
        PriceAfterOff = s.ProductSells.First().Price,
        ImageName = FileDirectories.ProductImageDirectory500 + s.ImageName,
        Price = s.ProductSells.First().Price,
        Shop = s.ProductSells.First().Seller.Title,
        Slug = s.Slug
    }).ToList();
        if (model.Count > 0)
        {
            model.ForEach(x =>
            {
                if (_discountContext.ProductDiscounts.Any(p => p.ProductId == x.ProductId && p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date))
                {
                    var discount = _discountContext.ProductDiscounts.OrderByDescending(p => p.Percent)
                    .First(p => p.ProductId == x.ProductId && p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date);
                    if (discount.ProductSellId > 0)
                    {
                        var sellProduct = _shopContext.ProductSells.Find(discount.ProductSellId);
                        x.Shop = _shopContext.Sellers.Find(sellProduct.SellerId).Title;
                        x.Price = sellProduct.Price;
                        x.PriceAfterOff = sellProduct.Price - (discount.Percent * sellProduct.Price / 100);
                    }
                    else
                    {
                        x.PriceAfterOff = x.Price - (discount.Percent * x.Price / 100);
                    }
                }
            });
        }
        return model;
    }
    public List<WishListProductQueryModel> GetWishListForUserFromCppkie(List<int> productIds)
    {
        var model = _shopContext.Products
           .Include(c => c.WishLists)
   .Include(p => p.ProductSells).ThenInclude(s => s.OrderItems)
   .Include(p => p.ProductSells).ThenInclude(s => s.Seller)
   .Where(p => p.Active && productIds.Any(w => w == p.Id))
   .AsEnumerable()
   .OrderByDescending(p => p.ProductSells.Sum(s => s.OrderItems.Count))
   .Take(10).Select(s => new WishListProductQueryModel
   {
       ProductId = s.Id,
       Title = s.Title,
       Amount = s.ProductSells.Sum(s => s.Amount),
       ImageAlt = s.ImageAlt,
       PriceAfterOff = s.ProductSells.First().Price,
       ImageName = FileDirectories.ProductImageDirectory500 + s.ImageName,
       Price = s.ProductSells.First().Price,
       Shop = s.ProductSells.First().Seller.Title,
       Slug = s.Slug
   }).ToList();
        if (model.Count > 0)
        {
            model.ForEach(x =>
            {
                if (_discountContext.ProductDiscounts.Any(p => p.ProductId == x.ProductId && p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date))
                {
                    var discount = _discountContext.ProductDiscounts.OrderByDescending(p => p.Percent)
                    .First(p => p.ProductId == x.ProductId && p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date);
                    if (discount.ProductSellId > 0)
                    {
                        var sellProduct = _shopContext.ProductSells.Find(discount.ProductSellId);
                        x.Shop = _shopContext.Sellers.Find(sellProduct.SellerId).Title;
                        x.Price = sellProduct.Price;
                        x.PriceAfterOff = sellProduct.Price - (discount.Percent * sellProduct.Price / 100);
                    }
                    else
                    {
                        x.PriceAfterOff = x.Price - (discount.Percent * x.Price / 100);
                    }
                }
            });
        }
        return model;
    }
    public List<AmazingSliderQueryModel> GetAmazingSliderData()
    {
        List<AmazingSliderQueryModel> model1 = new List<AmazingSliderQueryModel>();
        List <AmazingSliderQueryModel> model = _discountContext.ProductDiscounts.
            Where(p => p.EndDate.Date >= DateTime.Now.Date && p.ProductSellId == 0)
            .Take(9).Select(r => new AmazingSliderQueryModel
        {
            Amount = 0,
            ImageAlt = "",
            PriceAfterOff = 0,
            EndDate = r.EndDate,
            Features = new List<FeatureForProductSingleQueryModel>(),
            Id = r.ProductId,
            ImageName = "",
            Percent = r.Percent,
            Price = 0,
            Slug = "",
            IsFinished = false,
            Title = "",
            isWishList = false
            }).ToList();
        if (model.Count() < 9)
        {
            var x = 9 - model.Count();
            var result = _discountContext.ProductDiscounts.Where(p => p.EndDate.Date < DateTime.Now.Date && p.ProductSellId == 0)
                .OrderByDescending(c => c.EndDate).Take(x).Select(r => new AmazingSliderQueryModel
                {
                    Amount = 0,
                    ImageAlt = "",
                    PriceAfterOff = 0,
                    EndDate = r.EndDate,
                    Features = new List<FeatureForProductSingleQueryModel>(),
                    Id = r.ProductId,
                    ImageName = "",
                    Percent = r.Percent,
                    Price = 0,
                    Slug = "",
                    IsFinished = true,
                    Title = "",
                    isWishList = false
                }).ToList();
            model.AddRange(result);
        }
        foreach (var item in model)
        {
            var product = _shopContext.Products.Include(p => p.ProductFeatures).
                Include(p => p.ProductSells).Single(p=>p.Id == item.Id);
            if (product.ProductSells.Count() > 0)
            {
                item.Slug = product.Slug;
                item.Amount = product.ProductSells.Sum(c => c.Amount);
                item.ImageAlt = product.ImageAlt;
                item.Features = product.ProductFeatures.Take(4)
                    .Select(f => new FeatureForProductSingleQueryModel
                    {
                        Title = f.Title,
                        Value = f.Value
                    }).ToList();
                item.ImageName = FileDirectories.ProductImageDirectory500 + product.ImageName;
                item.Price = product.ProductSells.First().Price;
                item.Title = product.Title;
                item.PriceAfterOff = item.Price - (item.Percent * item.Price / 100);
                var sellerId = product.ProductSells.First().SellerId;
                item.Shop = _shopContext.Sellers.Find(sellerId).Title;
                model1.Add(item);
            }
        }
        return model1;
    }

    public List<WishListForUserPanelQueryModel> GetLastWishListForUserPanel(int userId) =>
        _shopContext.WishLists.Include(w => w.Product)
            .ThenInclude(p => p.ProductSells)
            .Where(w => w.UserId == userId).Take(4)
            .Select(w => new WishListForUserPanelQueryModel
            {
                Amount = w.Product.ProductSells.Sum(s => s.Amount),
                ImageAddress = FileDirectories.ProductImageDirectory100 + w.Product.ImageName,
                ImageAlt = w.Product.ImageAlt,
                Id = w.Id,
                ProductId = w.ProductId,
                Slug = w.Product.Slug,
                Title = w.Product.Slug
            }).ToList();

    public List<AjaxSearchModel> SearchAjax(string filter) =>
        _shopContext.Products.Where(p => p.Title.ToLower().Contains(filter.Trim().ToLower()))
        .Select(p => new AjaxSearchModel
        {
            ImageAddress = FileDirectories.ProductImageDirectory100 + p.ImageName,
            id = p.Id,
            Slug = p.Slug,
            Title = p.Title
        }).ToList();
}

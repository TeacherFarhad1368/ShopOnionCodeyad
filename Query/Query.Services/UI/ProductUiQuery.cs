using Blogs.Domain.BlogCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Query.Contract.UI;
using Query.Contract.UI.Product;
using Seos.Domain;
using Seos.Infrastructure;
using Shared.Application;
using Shared.Domain.Enum;
using Shop.Infrastructure;

namespace Query.Services.UI;
internal class ProductUiQuery : IProductUiQuery
{
    private readonly ShopContext _shopContext;
    private readonly ISeoRepository _seoRepository;
    public ProductUiQuery(ShopContext shopContext, ISeoRepository seoRepository)
    {
        _shopContext = shopContext;
        _seoRepository = seoRepository; 
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
            return new();
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
                    PriceAfterOff = p.ProductSells.OrderByDescending(b => b.OrderItems.Count).First().Price -1,
                    Id = p.Id,
                    ImageName = FileDirectories.ProductImageDirectory500 + p.ImageName,
                    Price = p.ProductSells.OrderByDescending(b => b.OrderItems.Count).First().Price,
                    Shop = p.ProductSells.OrderByDescending(b=>b.OrderItems.Count).First().Seller.Title,
                    Slug = p.Slug,
                    Title = p.Title
                }).ToList();
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
        var seo = _seoRepository.GetSeoForUi(ownerSeoId, WhereSeo.BlogCategory, seoTitle);
        model.Seo = new(seo.MetaTitle, seo.MetaDescription, seo.MetaKeyWords, seo.IndexPage, seo.Canonical, seo.Schema);
        return model;
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Query.Contract.UI.Product;
using Shop.Infrastructure;

namespace Query.Services.UI;
internal class ProductUiQuery : IProductUiQuery
{
    private readonly ShopContext _shopContext;

    public ProductUiQuery(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public ShopPaging GetProductsForUi(int pageId, string filter, string categorySlug, int Id, ShopOrderBy orderBy)
    {
        string shopTitle = "";
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
                shopTitle = seller.Title;
            }
        }
        if (!string.IsNullOrEmpty(categorySlug))
        {
            var category = _shopContext.ProductCategories.SingleOrDefault(c => c.Slug == categorySlug);
            if(category != null)
            {
                res = res.Where(r => r.ProductCategoryRelations.Any(s => s.ProductCategoryId == category.Id)).OrderBy(p => p.Id);
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
                res = res.OrderByDescending(p => p.ProductSells.Sum(s=>s.OrderItems.Count()));
                break;
            case ShopOrderBy.ارزان_ترین:
                res = res.OrderBy(p => p.ProductSells.First().Price);
                break;
            case ShopOrderBy.گران_ترین:
                res = res.OrderByDescending(p => p.ProductSells.First().Price);
                break;
        }

        ShopPaging model = new();
        model.GetData(res, pageId, 9, 2);
        model.Filter = filter;
        model.ShopId = Id;
        model.ShopTitle = shopTitle;
        model.CategorySlug = categorySlug;
        model.OrderBy = orderBy;
        model.Products = new List<ProductShopUiQueryModel>();
        if(res.Count() > 0)
        {
            model.Products = res.Skip(model.Skip).Take(model.Take)
                .Select(p => new ProductShopUiQueryModel
                {
                    ImageAlt = p.ImageAlt,
                    PriceAfterOff = p.ProductSells.First().Price,
                    Id = p.Id,
                    ImageName = p.ImageName,
                    Price = p.ProductSells.First().Price,
                    Shop = p.ProductSells.First().Seller.Title,
                    Slug = p.Slug,
                    Title = p.Title
                }).ToList();
        }
        return model;
    }
}

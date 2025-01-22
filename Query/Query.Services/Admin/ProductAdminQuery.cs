using Discounts.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Contract.Admin.Product;
using Shared.Application;
using Shop.Domain.ProductAgg;
using Shop.Infrastructure;

namespace Query.Services.Admin;
internal class ProductAdminQuery : IProductAdminQuery
{
    private readonly ShopContext _context;
    private readonly DiscountContext _discountContext;

    public ProductAdminQuery(ShopContext context, DiscountContext discountContext)
    {
        _context = context;
        _discountContext = discountContext;
    }

    public ProductAdminPaging GetProductsForAdmin(int pageId, int take, int categoryId, string filter, ProductAdminOrderBy orderBy)
    {
        string pageTitle = "لیست محصولات";
        IQueryable<Product> res = _context.Products.Include(p => p.ProductCategoryRelations);
        if (categoryId > 0)
        {
            var category = _context.ProductCategories.SingleOrDefault(c => c.Id == categoryId);
            if (category != null)
            {
                res = res.Where(r => r.ProductCategoryRelations.Count(o => o.ProductCategoryId == categoryId) > 0);
                pageTitle = pageTitle + " برای دسته بندی " + category.Title;
            }
        }
        if (!string.IsNullOrEmpty(filter))
        {
            res = res.Where(r => r.Title.ToLower().Contains(filter.ToLower()) ||
            r.Description.ToLower().Contains(filter.ToLower()));
            pageTitle = pageTitle + " شامل عبارت :  " + filter;
        }
        switch (orderBy)
        {
            case ProductAdminOrderBy.تاریخ_ثبت_از_اول:
                res = res.OrderBy(p => p.Id);
                break;
            case ProductAdminOrderBy.تاریخ_ثبت_از_آخر:
                res = res.OrderByDescending(p => p.Id);
                break;
            default:
                break;
        }
        ProductAdminPaging model = new();
        model.GetData(res, pageId, take, 2);
        model.Filter = filter;
        model.CategoryId = categoryId;
        model.PageTitle = pageTitle;
        model.OrderBy = orderBy;
        model.Products = new();
        if (res.Count() > 0)
        {
            model.Products = res.Skip(model.Skip).Take(model.Take)
                .Select(p => new ProductQueryAdminModel
                {
                    Active = p.Active,
                    ImageAlt = p.ImageAlt,
                    CreationDate = p.CreateDate.ToPersainDate(),
                    UpdateDate = p.UpdateDate.ToPersainDate(),
                    Id = p.Id,
                    ImageName = p.ImageName,
                    Slug = p.Slug,
                    Title = p.Title,
                    Weight = p.Weight,
                }).ToList();
            model.Products.ForEach(x =>
            {
                var productDiscounts = _discountContext.ProductDiscounts.Where(p => (p.ProductId == x.Id && p.ProductSellId == 0)
                && (p.StartDate.Date <= DateTime.Now.Date && p.EndDate.Date >= DateTime.Now.Date));
                if (productDiscounts.Any())
                {
                    var dis = productDiscounts.OrderBy(p => p.Id).Last();
                    x.ProductDiscountPercent = dis.Percent;
                }
            });
        }
        return model;
    }

}

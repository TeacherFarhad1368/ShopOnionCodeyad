using Microsoft.EntityFrameworkCore;
using Shared.Application;
using Shop.Application.Contract.ProductApplication;
using Shop.Domain.ProductAgg;
using Shop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Services
{
    internal class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;

        public ProductQuery(ShopContext context)
        {
            _context = context;
        }

        public List<ProductForAddProductSellQueryModel> GetProductsForAddProductSells(int id)
        {
            return _context.Products.Include(c=>c.ProductCategoryRelations)
                .Where(c=>c.ProductCategoryRelations.Any(r=>r.ProductCategoryId == id))
                .Select(c=> new ProductForAddProductSellQueryModel
                {
                    Id = c.Id,
                    Title = c.Title
                }).ToList();
        }

        public ProductAdminPaging GetProductsForAdmin(int pageId, int take, int categoryId, string filter, ProductAdminOrderBy orderBy)
        {
            string pageTitle = "لیست محصولات";
            IQueryable<Product> res = _context.Products.Include(p=>p.ProductCategoryRelations);
            if(categoryId > 0)
            {
                var category = _context.ProductCategories.SingleOrDefault(c=>c.Id == categoryId);
                if(category != null)
                {
                    res = res.Where(r => r.ProductCategoryRelations.Count(o => o.ProductCategoryId == categoryId) > 0) ;
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
                    res = res.OrderBy(p=>p.Id);
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
            if(res.Count() > 0)
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
            }
            return model;
        }
    }
}

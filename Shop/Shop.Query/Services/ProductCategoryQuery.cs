﻿using Microsoft.EntityFrameworkCore;
using Shared.Application;
using Shop.Application.Contract.ProductCategoryApplication.Query;
using Shop.Domain.ProductCategoryAgg;

namespace Shop.Query.Services;
internal class ProductCategoryQuery : IProductCategoryQuery
{
    private readonly IProductCategoryRepository _productCategoryRepository;

    public ProductCategoryQuery(IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<bool> CheckCategoryHaveParent(int id)
    {
        var category = await _productCategoryRepository.GetByIdAsync(id);
        return category != null && category.Parent > 0;
    }

    public async Task<List<ProductCategoryForAddProduct>> GetCategoriesForAddProduct()
    {
        var res = _productCategoryRepository.GetAllQuery();
        return await res.Select(r => new ProductCategoryForAddProduct
        {
            Id = r.Id,
            Parent = r.Parent,
            Title = r.Title
        }).ToListAsync();
    }

    public ProductCategoryAdminPageQueryModel GetCategoriesForAdmin(int id)
    {
        List<ProductCategoryAdminQueryModel> productCategories = new List<ProductCategoryAdminQueryModel>();
        string title = "لیست دسته بندی های سر گروه";
        var res = _productCategoryRepository.GetAllByQuery(c => c.Parent == id);
        productCategories = res.Select(r => new ProductCategoryAdminQueryModel(r.Id, r.Title, r.ImageName, r.CreateDate.ToPersainDate(), r.UpdateDate.ToPersainDate(), r.Active)).ToList();
        if (id > 0)
        {
            var category = _productCategoryRepository.GetById(id);
            title = $"لیست زیر دسته های {category.Title}";
        }
        ProductCategoryAdminPageQueryModel model = new ProductCategoryAdminPageQueryModel(id,title,productCategories);
        return model;   
    }

    public List<ProductCategoryForAddProductSell> GetCategoryForAddProductSells(int id)
    {
        var res = _productCategoryRepository.GetAllByQuery(c=>c.Parent == id);
        return res.Select(r => new ProductCategoryForAddProductSell
        {
            Id = r.Id,  
            Title = r.Title
        }).ToList();
    }
}

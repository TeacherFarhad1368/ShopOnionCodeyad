﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Infrastructure;
using Shop.Domain.ProductAgg;

namespace Shop.Infrastructure.Services;

internal class ProductRepository : Repository<int, Product>, IProductRepository
{
    private readonly ShopContext _context;
    public ProductRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.Include(p => p.ProductCategoryRelations)
            .SingleOrDefaultAsync(p => p.Id == id);
    }
}

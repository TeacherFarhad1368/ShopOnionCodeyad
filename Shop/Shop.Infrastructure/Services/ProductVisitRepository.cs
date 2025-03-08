using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using Shop.Domain.ProductVisitAgg;

namespace Shop.Infrastructure.Services;

internal class ProductVisitRepository : Repository<int, ProductVisit>, IProductVisitRepository
{
    private readonly ShopContext _context;
    public ProductVisitRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> UbsertProductVisitAsymc(ProductVisit command)
    {
        if(await ExistByAsync(c=>c.ProductId == command.ProductId && c.UserId == command.UserId))
        {
            ProductVisit visit = await _context.ProductVisits.SingleAsync
                (c => c.ProductId == command.ProductId && c.UserId == command.UserId);
            visit.AddVisit();
            return await SaveAsync();
        }
        else
        {
            return await CreateAsync(command);
        }
    }
}

using Shared.Infrastructure;
using Stores.Domain.StoreProductAgg;
namespace Stores.Infrastructure.Services;

internal class StoreProductRepository : Repository<int, StoreProduct>, IStoreProductRepository
{
    private readonly StoreContext _context;
    public StoreProductRepository(StoreContext context) : base(context)
    {
        _context = context;
    }
}
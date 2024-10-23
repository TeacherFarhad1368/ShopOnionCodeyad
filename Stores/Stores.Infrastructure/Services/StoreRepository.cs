using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using Stores.Domain.StoreAgg;
namespace Stores.Infrastructure.Services;
internal class StoreRepository : Repository<int, Store>, IStoreRepository
{
    private readonly StoreContext _context;
    public StoreRepository(StoreContext context) : base(context)
    {
        _context = context;
    }
}

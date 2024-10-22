using Shared.Infrastructure;
using Shop.Domain.SellerPackageAgg;

namespace Shop.Infrastructure.Services;

internal class SellerPackageRepository : Repository<int, SellerPackage>, ISellerPackageRepository
{
    private readonly ShopContext _context;
    public SellerPackageRepository(ShopContext context) : base(context)
    {
        _context = context;
    }
}

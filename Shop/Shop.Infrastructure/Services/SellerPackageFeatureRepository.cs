using Shared.Infrastructure;
using Shop.Domain.SellerPackageFeatureAgg;

namespace Shop.Infrastructure.Services;

internal class SellerPackageFeatureRepository : Repository<int, SellerPackageFeature>, ISellerPackageFeatureRepository
{
    private readonly ShopContext _context;
    public SellerPackageFeatureRepository(ShopContext context) : base(context)
    {
        _context = context;
    }
}
using Shared.Infrastructure;
using Shop.Domain.ProductGalleryAgg;

namespace Shop.Infrastructure.Services;

internal class ProductGalleryRepository : Repository<int, ProductGallery>, IProductGalleryRepository
{
    private readonly ShopContext _context;
    public ProductGalleryRepository(ShopContext context) : base(context)
    {
        _context = context;
    }
}

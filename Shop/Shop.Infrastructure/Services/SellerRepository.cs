using Shared.Infrastructure;
using Shop.Domain.SellerAgg;

namespace Shop.Infrastructure.Services;

internal class SellerRepository : Repository<int, Seller>, ISellerRepository
{
    private readonly ShopContext _context;
    public SellerRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public Seller? GetSellerForUserPanel(int id, int userId)
    {
        return _context.Sellers.SingleOrDefault(s=>s.Id == id && s.UserId == userId);   
    }
}

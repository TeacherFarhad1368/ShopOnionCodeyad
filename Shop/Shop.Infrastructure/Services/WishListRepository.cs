using Shared.Infrastructure;
using Shop.Domain.WishListAgg;

namespace Shop.Infrastructure.Services;

internal class WishListRepository : Repository<int, WishList>, IWishListRepository
{
    private readonly ShopContext _context;
    public WishListRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> DeleteListAsync(List<WishList> wishes)
    {
        if (wishes.Count == 0) return false;
        _context.WishLists.RemoveRange(wishes);
        return await SaveAsync();
    }

    public bool DeleteUserProductWishList(int userId, int id)
    {
        var wish = _context.WishLists.SingleOrDefault(s=>s.ProductId == id && s.UserId == userId);
        if(wish == null) return false;
        return Delete(wish);
    }
}

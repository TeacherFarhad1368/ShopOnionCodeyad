using Shared.Domain;

namespace Shop.Domain.WishListAgg;

public interface IWishListRepository : IRepository<int, WishList>
{
    Task<bool> DeleteListAsync(List<WishList> wishes);
}

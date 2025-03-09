using Shop.Application.Contract.WishListApplication.Query;
using Shop.Domain.WishListAgg;

namespace Shop.Query.Services;
internal class WishListQuery : IWishListQuery
{
    private readonly IWishListRepository _wishListRepository;

    public WishListQuery(IWishListRepository wishListRepository)
    {
        _wishListRepository = wishListRepository;
    }

    public int GetUserWishListCount(int userId)
    {
        return _wishListRepository.GetAllByQuery(c=>c.UserId == userId).Count();
    }

    public bool IsUserHaveProductWishList(int UserId, int productId)=>
        _wishListRepository.ExistBy(c=>c.ProductId == productId && c.UserId == UserId);
}

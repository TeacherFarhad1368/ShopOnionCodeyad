
namespace Shop.Application.Contract.WishListApplication.Query;
public interface IWishListQuery
{
    int GetUserWishListCount(int userId);
    bool IsUserHaveProductWishList(int UserId, int productId);
}
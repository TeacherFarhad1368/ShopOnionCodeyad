namespace Shop.Application.Contract.WishListApplication.Command;
public interface IWishListApplication
{
    Task<bool> UbsertWishListsAsync(List<CreateWishList> list);
    Task<bool> DeleteWishList(int id,int userId);
}

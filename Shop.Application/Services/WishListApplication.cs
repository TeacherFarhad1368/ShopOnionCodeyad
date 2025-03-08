using Shop.Application.Contract.WishListApplication.Command;
using Shop.Domain.WishListAgg;
using System.ComponentModel.Design;

namespace Shop.Application.Services;
internal class WishListApplication : IWishListApplication
{
    private readonly IWishListRepository _wishListRepository;

    public WishListApplication(IWishListRepository wishListRepository)
    {
        _wishListRepository = wishListRepository;
    }

    public async Task<bool> DeleteWishList(int id, int userId)
    {
        var wish = await _wishListRepository.GetByIdAsync(id);
        if (wish.UserId == userId)
            return await _wishListRepository.DeleteAsync(wish);
        return false;
    }

    public async Task<bool> UbsertWishListsAsync(List<CreateWishList> list)
    {
        List<WishList>  wishes= new List<WishList>();
        if (list.Count > 0)
        {
            foreach (var item in list)
            {
                if(await _wishListRepository.ExistByAsync(w=>w.UserId == item.UserId && w.ProductId == item.ProductId) == false)
                    wishes.Add(new WishList(item.ProductId, item.UserId));
            }
            if (wishes.Count > 0)
                return await _wishListRepository.DeleteListAsync(wishes);
            else return false;
        }
        else return false;

    }
}

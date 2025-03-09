using Shop.Application.Contract.WishListApplication.Command;
using Shop.Domain.ProductAgg;
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

    public bool AddUserProductWishList(int userId, int id)
    {
        if (_wishListRepository.ExistBy(c => c.ProductId == id && c.UserId == userId))
        {
            return _wishListRepository.DeleteUserProductWishList(userId, id);
        }
        else
        {
            WishList wish = new WishList(id, userId);
            return _wishListRepository.Create(wish);
        }
    }

    public bool AddUsersWishList(int userId, List<int> wishesIds)
    {
        if (wishesIds.Count > 0)
            foreach (var item in wishesIds)
                if (_wishListRepository.ExistBy(w => w.UserId == userId && w.ProductId == item) == false)
                    _wishListRepository.Create(new WishList(item, userId));
        return true;
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

using Microsoft.AspNetCore.Mvc;
using Query.Contract.UI.Product;
using Shared.Application.Services.Auth;
using Shop.Application.Contract.WishListApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class NewProductViewComponent : ViewComponent
{
    private readonly IProductUiQuery _productUiQuery;
    private readonly IAuthService _authService;
    private readonly IWishListQuery _wishListQuery;
    public NewProductViewComponent(IProductUiQuery productUiQuery, IAuthService authService, IWishListQuery wishListQuery)
    {
        _productUiQuery = productUiQuery;
        _authService = authService;
        _wishListQuery = wishListQuery;
    }
    public IViewComponentResult Invoke()
    {
        var model = _productUiQuery.GetNewPeoductForIndex();
        var userId = _authService.GetLoginUserId();
        foreach (var item in model)
        {
            if (userId == 0)
            {
                string cookieName = "boloorShop-wishList-items";
                if (Request.Cookies.TryGetValue(cookieName, out var cartJson))
                {
                    List<int> wishesIds = System.Text.Json.JsonSerializer.Deserialize<List<int>>(cartJson);
                    if (wishesIds.Count > 0)
                    {
                        item.isWishList = wishesIds.Any(w => w == item.Id);
                    }
                    else item.isWishList = false;
                }
                else item.isWishList = false;
            }
            else
                item.isWishList = _wishListQuery.IsUserHaveProductWishList(userId, item.Id);
        }
        return View(model);
    }
}
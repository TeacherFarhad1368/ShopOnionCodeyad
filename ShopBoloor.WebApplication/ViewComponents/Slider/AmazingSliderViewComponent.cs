using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Query.Contract.UI.Product;
using Shared.Application.Services.Auth;
using Shared.Caching;
using Shop.Application.Contract.WishListApplication.Query;
using System.Threading.Tasks;

namespace ShopBoloor.WebApplication.ViewComponents;

public class AmazingSliderViewComponent : ViewComponent
{
    private readonly IProductUiQuery _productUiQuery;
    private readonly IAuthService _authService;
    private readonly IWishListQuery _wishListQuery;
    private readonly IDistributedCache _distributedCache;
    private readonly string _cacheKey = "Index_Amazing_Slider";
    public AmazingSliderViewComponent(IProductUiQuery productUiQuery, IAuthService authService, IWishListQuery wishListQuery,
        IDistributedCache distributedCache)
    {
        _productUiQuery = productUiQuery;
        _authService = authService;
        _wishListQuery = wishListQuery;
        _distributedCache = distributedCache;   
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await _distributedCache.GetFromSqlServerAsync<List<AmazingSliderQueryModel>>(_cacheKey);
        if(model == null)
        {
            model = _productUiQuery.GetAmazingSliderData();
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
            await _distributedCache.SetInSqlServerAsync(_cacheKey, model);
        }
        return View(model);
    }
}

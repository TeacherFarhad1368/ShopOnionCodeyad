using Microsoft.AspNetCore.Mvc;
using Query.Contract.UI.Product;
using Shared.Application.Services.Auth;

namespace ShopBoloor.WebApplication.ViewComponents;

public class UserPanelWishListViewComponent : ViewComponent
{
    private readonly IAuthService _authService;
    private readonly IProductUiQuery _productUiQuery;
    public UserPanelWishListViewComponent( IAuthService authService, IProductUiQuery productUiQuery)
    {
        _authService = authService;
        _productUiQuery = productUiQuery;   
    }
    public IViewComponentResult Invoke()
    {
        var userId = _authService.GetLoginUserId();
        var model = _productUiQuery.GetLastWishListForUserPanel(userId);
        return View(model);
    }
}
using Microsoft.AspNetCore.Mvc;
using Query.Contract.UserPanel.User;
using Shared.Application.Services.Auth;

namespace ShopBoloor.WebApplication.ViewComponents;

public class UserPanelSidebarViewComponent : ViewComponent
{
    private readonly IUserPanelQuery _query;
    private readonly IAuthService _authService;
    public UserPanelSidebarViewComponent(IUserPanelQuery query,IAuthService authService)
    {
        _query = query;
        _authService = authService;
    }
    public IViewComponentResult Invoke()
    {
        var userId = _authService.GetLoginUserId();
        var model = _query.GetUserPanelSideBarModel(userId);
        return View(model);
    }
}
using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin;
using Query.Contract.UI.Product;
using Shared.Application.Services.Auth;
using Shop.Application.Contract.WishListApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class AdminDataViewComponent : ViewComponent
{
    private readonly IAdminQuery _adminQuery;

    public AdminDataViewComponent(IAdminQuery adminQuery)
    {
        _adminQuery = adminQuery;
    }

    public IViewComponentResult Invoke()
    {
        var model = _adminQuery.GetAdminData();
        return View(model);
    }
}
public class AdminLastUserViewComponent : ViewComponent
{
    private readonly IAdminQuery _adminQuery;

    public AdminLastUserViewComponent(IAdminQuery adminQuery)
    {
        _adminQuery = adminQuery;
    }

    public IViewComponentResult Invoke()
    {
        var model = _adminQuery.GetLastUsersForAdmin();
        return View(model);
    }
}
public class AdminLastOrderViewComponent : ViewComponent
{
    private readonly IAdminQuery _adminQuery;

    public AdminLastOrderViewComponent(IAdminQuery adminQuery)
    {
        _adminQuery = adminQuery;
    }

    public IViewComponentResult Invoke()
    {
        var model = _adminQuery.GetLastOrdersForAdmin();
        return View(model);
    }
}

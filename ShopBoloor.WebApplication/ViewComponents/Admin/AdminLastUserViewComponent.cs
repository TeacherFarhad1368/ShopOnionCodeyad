using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin;

namespace ShopBoloor.WebApplication.ViewComponents;

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

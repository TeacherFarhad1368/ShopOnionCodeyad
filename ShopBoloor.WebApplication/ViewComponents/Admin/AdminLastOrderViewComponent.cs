using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin;

namespace ShopBoloor.WebApplication.ViewComponents;

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

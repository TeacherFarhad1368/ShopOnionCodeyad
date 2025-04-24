using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.MenuApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class MenuResponsiveIndexViewComponent : ViewComponent
{
    private readonly IMenuQuery _query;
    public MenuResponsiveIndexViewComponent(IMenuQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetForIndex();
        return View(model);
    }
}

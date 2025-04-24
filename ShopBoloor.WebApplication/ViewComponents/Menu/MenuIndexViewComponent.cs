using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.MenuApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class MenuIndexViewComponent : ViewComponent
{
    private readonly IMenuQuery _query;
    public MenuIndexViewComponent(IMenuQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetForIndex();
        return View(model);
    }
}

using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.MenuApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class MenuFooterViewComponent : ViewComponent
{
    private readonly IMenuQuery _query;
    public MenuFooterViewComponent(IMenuQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetForFooter();
        return View(model);
    }
}

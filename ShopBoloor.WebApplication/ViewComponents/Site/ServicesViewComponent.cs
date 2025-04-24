using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.SiteServiceApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class ServicesViewComponent : ViewComponent
{
    private readonly ISiteServiceQuery _query;
    public ServicesViewComponent(ISiteServiceQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetAllForUI();
        return View(model);
    }
}

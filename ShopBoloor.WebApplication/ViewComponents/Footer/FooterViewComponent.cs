using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.SiteSettingApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class FooterViewComponent : ViewComponent
{
    private readonly ISiteSettingQuery _query;
    public FooterViewComponent(ISiteSettingQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetFooter();
        return View(model);
    }
}

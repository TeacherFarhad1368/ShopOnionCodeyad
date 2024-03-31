using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.SiteSettingApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class LogoHeaderViewComponent : ViewComponent
{
    private readonly ISiteSettingQuery _query;
    public LogoHeaderViewComponent(ISiteSettingQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetLogoForUi();
        return View(model);
    }
}

public class FavViewComponent : ViewComponent
{
    private readonly ISiteSettingQuery _query;
    public FavViewComponent(ISiteSettingQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetFavIconForUi();
        return View(model);
    }
}

using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.SiteSettingApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class SocialViewComponent : ViewComponent
{
    private readonly ISiteSettingQuery _query;
    public SocialViewComponent(ISiteSettingQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetSocialForUi();
        return View(model);
    }
}

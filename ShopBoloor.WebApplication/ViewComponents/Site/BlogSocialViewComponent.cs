using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.SiteSettingApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class BlogSocialViewComponent : ViewComponent
{
    private readonly ISiteSettingQuery _query;
    public BlogSocialViewComponent(ISiteSettingQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetSocialForUi();
        return View(model);
    }
}

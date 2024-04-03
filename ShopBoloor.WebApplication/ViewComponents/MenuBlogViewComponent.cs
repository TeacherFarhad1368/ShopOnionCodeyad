using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.MenuApplication.Query;
using Site.Application.Contract.SiteSettingApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class MenuBlogViewComponent : ViewComponent
{
    private readonly IMenuQuery _query;
    public MenuBlogViewComponent(IMenuQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetForBlog();
        return View(model);
    }
}
public class MenuBlogMobileViewComponent : ViewComponent
{
    private readonly IMenuQuery _query;
    private readonly ISiteSettingQuery _site;
    public MenuBlogMobileViewComponent(IMenuQuery query, ISiteSettingQuery site)
    {
        _site = site;
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var social = _site.GetSocialForUi();
        var menus = _query.GetForBlog();
        return View(Tuple.Create(menus,social));
    }
}
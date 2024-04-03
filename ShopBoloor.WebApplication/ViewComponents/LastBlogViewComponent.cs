using Blogs.Application.Contract.BlogApplication.Query;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.SiteSettingApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class LastBlogViewComponent : ViewComponent
{
    private readonly IBlogQuery _query;
    public LastBlogViewComponent(IBlogQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetLastBlogForMagUi();
        return View(model);
    }
}
public class BestBlogMagViewComponent : ViewComponent
{
    private readonly IBlogQuery _query;
    public BestBlogMagViewComponent(IBlogQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetBestBlogForUi();
        return View(model);
    }
}
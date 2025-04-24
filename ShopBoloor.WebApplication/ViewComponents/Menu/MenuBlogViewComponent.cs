using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.MenuApplication.Query;

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

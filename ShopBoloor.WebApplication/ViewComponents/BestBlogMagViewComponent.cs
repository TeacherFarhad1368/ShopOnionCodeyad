using Blogs.Application.Contract.BlogApplication.Query;
using Microsoft.AspNetCore.Mvc;

namespace ShopBoloor.WebApplication.ViewComponents;

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

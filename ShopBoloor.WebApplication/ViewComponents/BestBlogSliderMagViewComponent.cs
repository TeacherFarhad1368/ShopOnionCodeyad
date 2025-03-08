using Blogs.Application.Contract.BlogApplication.Query;
using Microsoft.AspNetCore.Mvc;

namespace ShopBoloor.WebApplication.ViewComponents;

public class BestBlogSliderMagViewComponent : ViewComponent
{
    private readonly IBlogQuery _query;
    public BestBlogSliderMagViewComponent(IBlogQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetBestBlogForSliderUi();
        return View(model);
    }
}
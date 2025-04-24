using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enum;
using Site.Application.Contract.BanerApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class LeftBanerBlogViewComponent : ViewComponent
{
    private readonly IBanerQuery _query;
    public LeftBanerBlogViewComponent(IBanerQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetForUi(1, BanerState.بنر_تکی_وبلاگ_280x230);
        return View(model);
    }
}

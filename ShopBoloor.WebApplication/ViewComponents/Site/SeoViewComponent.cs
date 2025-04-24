using Microsoft.AspNetCore.Mvc;
using Seos.Application.Contract;
using Shared.Domain.Enum;

namespace ShopBoloor.WebApplication.ViewComponents;

public class SeoViewComponent : ViewComponent
{
    private readonly ISeoQuery _seoQuery;

    public SeoViewComponent(ISeoQuery seoQuery)
    {
        _seoQuery = seoQuery;
    }
    public IViewComponentResult Invoke(int ownerId,WhereSeo where,string title)
    {
        var model = _seoQuery.GetSeo(ownerId,where,title);
        return View(model);
    }
}
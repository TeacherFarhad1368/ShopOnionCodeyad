using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enum;
using Site.Application.Contract.BanerApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class TopBanerViewComponent : ViewComponent
{
    private readonly IBanerQuery _query;
    public TopBanerViewComponent(IBanerQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetForUi(1,BanerState.بنر_تکی_باریک_بالا_1645x105);
        return View(model);
    }
}

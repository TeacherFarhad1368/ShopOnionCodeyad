using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enum;
using Site.Application.Contract.BanerApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class BottomBanerViewComponent : ViewComponent
{
    private readonly IBanerQuery _query;
    public BottomBanerViewComponent(IBanerQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetForUi(1, BanerState.بنر_تکی_پایین_1680x210);
        return View(model);
    }
}

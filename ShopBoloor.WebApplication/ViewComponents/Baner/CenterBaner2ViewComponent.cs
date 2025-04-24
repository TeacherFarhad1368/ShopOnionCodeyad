using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enum;
using Site.Application.Contract.BanerApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class CenterBaner2ViewComponent : ViewComponent
{
    private readonly IBanerQuery _query;
    public CenterBaner2ViewComponent(IBanerQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetForUi(2, BanerState.بنر_2تایی_وسط_820x328);
        return View(model);
    }
}

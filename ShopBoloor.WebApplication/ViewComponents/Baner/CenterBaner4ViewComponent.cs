using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enum;
using Site.Application.Contract.BanerApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class CenterBaner4ViewComponent : ViewComponent
{
    private readonly IBanerQuery _query;
    public CenterBaner4ViewComponent(IBanerQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetForUi(4, BanerState.بنر_4تایی_وسط_400x300);
        return View(model);
    }
}

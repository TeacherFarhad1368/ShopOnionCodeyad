using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.SliderApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

public class SliderViewComponent : ViewComponent
{
    private readonly ISliderQuery _query;
    public SliderViewComponent(ISliderQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetAllForUi();
        return View(model);
    }
}

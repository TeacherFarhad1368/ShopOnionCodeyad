using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enum;
using Site.Application.Contract.BanerApplication.Query;
using Site.Application.Contract.SiteSettingApplication.Query;

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
public class LeftBanerViewComponent : ViewComponent
{
    private readonly IBanerQuery _query;
    public LeftBanerViewComponent(IBanerQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetForUi(2,BanerState.بنر_2تایی_چپ_850x420);
        return View(model);
    }
}
public class RightBanerViewComponent : ViewComponent
{
    private readonly IBanerQuery _query;
    public RightBanerViewComponent(IBanerQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetForUi(2, BanerState.بنر_2تایی_راست_850x420);
        return View(model);
    }
}
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
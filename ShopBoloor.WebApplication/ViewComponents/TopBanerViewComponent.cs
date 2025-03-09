using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enum;
using Site.Application.Contract.BanerApplication.Query;
using Site.Application.Contract.MenuApplication.Query;
using Site.Application.Contract.SiteServiceApplication.Query;
using Site.Application.Contract.SiteSettingApplication.Query;
using Site.Application.Contract.SliderApplication.Query;

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
public class ButtomBanerBlogViewComponent : ViewComponent
{
    private readonly IBanerQuery _query;
    public ButtomBanerBlogViewComponent(IBanerQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetForUi(1, BanerState.بنر_تکی_وبلاگ_1020x130);
        return View(model);
    }
}
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
public class MenuIndexViewComponent : ViewComponent
{
    private readonly IMenuQuery _query;
    public MenuIndexViewComponent(IMenuQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetForIndex();
        return View(model);
    }
}
public class MenuFooterViewComponent : ViewComponent
{
    private readonly IMenuQuery _query;
    public MenuFooterViewComponent(IMenuQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetForFooter();
        return View(model);
    }
}
public class ServicesViewComponent : ViewComponent
{
    private readonly ISiteServiceQuery _query;
    public ServicesViewComponent(ISiteServiceQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetAllForUI();
        return View(model);
    }
}
public class FooterViewComponent : ViewComponent
{
    private readonly ISiteSettingQuery _query;
    public FooterViewComponent(ISiteSettingQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetFooter();
        return View(model);
    }
}
public class ContactFooterViewComponent : ViewComponent
{
    private readonly ISiteSettingQuery _query;
    public ContactFooterViewComponent(ISiteSettingQuery query)
    {
        _query = query;
    }
    public IViewComponentResult Invoke()
    {
        var model = _query.GetContactDataForFooter();
        return View(model);
    }
}
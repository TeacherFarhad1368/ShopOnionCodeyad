using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.SiteSettingApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents;

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
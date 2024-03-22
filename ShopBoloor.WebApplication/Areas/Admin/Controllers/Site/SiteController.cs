using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.ImageSiteApplication.Command;
using Site.Application.Contract.ImageSiteApplication.Query;
using Site.Application.Contract.SiteSettingApplication.Command;
using Site.Application.Contract.SliderApplication.Command;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Site
{
	[Area("Admin")]
	public class SiteController : Controller
	{
		private readonly ISiteSettingApplication _siteSettingApplication;
		private readonly IImageSiteApplication _imageSiteApplication;
		private readonly IImageSiteQuery _imageSiteQuery;

		public SiteController(ISiteSettingApplication siteSettingApplication, IImageSiteApplication imageSiteApplication, IImageSiteQuery imageSiteQuery)
		{
			_siteSettingApplication = siteSettingApplication;
			_imageSiteApplication = imageSiteApplication;
			_imageSiteQuery = imageSiteQuery;
		}

		public IActionResult Index()=> View(_siteSettingApplication.GetForUbsert());
		[HttpPost]
		public IActionResult Index(UbsertSiteSetting model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _siteSettingApplication.Ubsert(model);
			if (res.Success)
			{
				TempData["ok"] = true;
				return RedirectToAction("Index");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}

		public IActionResult Images(int pageId = 1, int take = 10, string filter = "") =>
			   View(_imageSiteQuery.GetAllForAdmin(pageId, take, filter));


	}
}

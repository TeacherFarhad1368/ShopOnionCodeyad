using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Application;
using ShopBoloor.WebApplication.Utility;
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

        [PermissionChecker(Shared.Domain.Enum.UserPermission.مدیریت_سایت)]
        public IActionResult Index()=> View(_siteSettingApplication.GetForUbsert());
		[HttpPost]
        [PermissionChecker(Shared.Domain.Enum.UserPermission.مدیریت_سایت)]
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

        [PermissionChecker(Shared.Domain.Enum.UserPermission.آپلود_تصویر)]
        public IActionResult Images(int pageId = 1, int take = 10, string filter = "") =>
			   View(_imageSiteQuery.GetAllForAdmin(pageId, take, filter));
        [PermissionChecker(Shared.Domain.Enum.UserPermission.آپلود_تصویر)]
        public IActionResult CreateImage() => View();
        [PermissionChecker(Shared.Domain.Enum.UserPermission.آپلود_تصویر)]
        [HttpPost]
		public IActionResult CreateImage(CreateImageSite model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _imageSiteApplication.Create(model);
			if (res.Success)
			{
				TempData["ok"] = true;
				return RedirectToAction("Images");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
        }
        [PermissionChecker(Shared.Domain.Enum.UserPermission.حذف_تصویر)]
        public bool DeleteImage(int id) => _imageSiteApplication.DeleteFromDataBase(id);

    }
}

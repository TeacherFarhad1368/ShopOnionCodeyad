using Site.Application.Contract.SitePageApplication.Command;
using Site.Application.Contract.SitePageApplication.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Services.Auth;
using ShopBoloor.WebApplication.Utility;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Site
{
	[Area("Admin")]
    [PermissionChecker(Shared.Domain.Enum.UserPermission.مدیریت_صفحات)]
    public class SitePageController : Controller
	{
		private readonly ISitePageQuery _SitePageQuery;
		private readonly ISitePageApplication _SitePageApplication;
		private readonly IAuthService _authService;
		public SitePageController(ISitePageQuery SitePageQuery, ISitePageApplication SitePageApplication, IAuthService
			authService)
		{
			_SitePageQuery = SitePageQuery;
			_SitePageApplication = SitePageApplication;
			_authService = authService;
		}
		public IActionResult Index() => View(_SitePageQuery.GetAllForAdmin());
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(CreateSitePage model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _SitePageApplication.Create(model);
			if (res.Success)
			{
				TempData["ok"] = true;
				return RedirectToAction("Index");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}
		public IActionResult Edit(int id)
		{
			if (id == 2) return NotFound();
			var model = _SitePageApplication.GetForEdit(id);
			return View(model);
		}
		[HttpPost]
		public IActionResult Edit(int id, EditSitePage model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _SitePageApplication.Edit(model);
			if (res.Success)
			{
				TempData["ok"] = true;
				return Redirect($"/Admin/SitePage/Edit/{id}");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}
		public bool Active(int id) => _SitePageApplication.ActivationChange(id);
	}
}

using Site.Application.Contract.SiteServiceApplication.Command;
using Site.Application.Contract.SiteServiceApplication.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Services.Auth;
using ShopBoloor.WebApplication.Utility;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Site
{
	[Area("Admin")]
    [PermissionChecker(Shared.Domain.Enum.UserPermission.مدیریت_سایت)]
    public class SiteServiceController : Controller
	{
		private readonly ISiteServiceQuery _SiteServiceQuery;
		private readonly ISiteServiceApplication _SiteServiceApplication;
		private readonly IAuthService _authService;
		public SiteServiceController(ISiteServiceQuery SiteServiceQuery, ISiteServiceApplication SiteServiceApplication, IAuthService
			authService)
		{
			_SiteServiceQuery = SiteServiceQuery;
			_SiteServiceApplication = SiteServiceApplication;
			_authService = authService;
		}
		public IActionResult Index() => View(_SiteServiceQuery.GetAllForAdmin());
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(CreateSiteService model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _SiteServiceApplication.Create(model);
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
			var model = _SiteServiceApplication.GetForEdit(id);
			return View(model);
		}
		[HttpPost]
		public IActionResult Edit(int id, EditSiteService model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _SiteServiceApplication.Edit(model);
			if (res.Success)
			{
				TempData["ok"] = true;
				return RedirectToAction("Index");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}
		public bool Active(int id) => _SiteServiceApplication.ActivationChange(id);
	}
}

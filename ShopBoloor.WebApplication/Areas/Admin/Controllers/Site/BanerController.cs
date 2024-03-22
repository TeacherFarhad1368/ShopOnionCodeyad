using Site.Application.Contract.BanerApplication.Command;
using Site.Application.Contract.BanerApplication.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Services.Auth;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Site
{
	[Area("Admin")]
	public class BanerController : Controller
	{
		private readonly IBanerQuery _BanerQuery;
		private readonly IBanerApplication _BanerApplication;
		private readonly IAuthService _authService;
		public BanerController(IBanerQuery BanerQuery, IBanerApplication BanerApplication, IAuthService
			authService)
		{
			_BanerQuery = BanerQuery;
			_BanerApplication = BanerApplication;
			_authService = authService;
		}
		public IActionResult Index() => View(_BanerQuery.GetAllForAdmin());
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(CreateBaner model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _BanerApplication.Create(model);
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
			var model = _BanerApplication.GetForEdit(id);
			return View(model);
		}
		[HttpPost]
		public IActionResult Edit(int id, EditBaner model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _BanerApplication.Edit(model);
			if (res.Success)
			{
				TempData["ok"] = true;
				return RedirectToAction("Index");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}
		public bool Active(int id) => _BanerApplication.ActivationChange(id);
	}
}

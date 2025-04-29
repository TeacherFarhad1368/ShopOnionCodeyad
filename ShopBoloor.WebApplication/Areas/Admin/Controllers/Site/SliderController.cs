using Site.Application.Contract.SliderApplication.Command;
using Site.Application.Contract.SliderApplication.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Services.Auth;
using ShopBoloor.WebApplication.Utility;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Site
{
	[Area("Admin")]
    [PermissionChecker(Shared.Domain.Enum.UserPermission.مدیریت_سایت)]
    public class SliderController : Controller
	{
		private readonly ISliderQuery _SliderQuery;
		private readonly ISliderApplication _SliderApplication;
		private readonly IAuthService _authService;
		public SliderController(ISliderQuery SliderQuery, ISliderApplication SliderApplication, IAuthService
			authService)
		{
			_SliderQuery = SliderQuery;
			_SliderApplication = SliderApplication;
			_authService = authService;
		}
		public IActionResult Index() => View(_SliderQuery.GetAllForAdmin());
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(CreateSlider model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _SliderApplication.Create(model);
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
			var model = _SliderApplication.GetForEdit(id);
			return View(model);
		}
		[HttpPost]
		public IActionResult Edit(int id, EditSlider model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _SliderApplication.Edit(model);
			if (res.Success)
			{
				TempData["ok"] = true;
				return RedirectToAction("Index");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}
		public bool Active(int id) => _SliderApplication.ActivationChange(id);
	}
}

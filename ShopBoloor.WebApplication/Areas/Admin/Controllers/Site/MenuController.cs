using Site.Application.Contract.MenuApplication.Command;
using Site.Application.Contract.MenuApplication.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Services.Auth;
using ShopBoloor.WebApplication.Utility;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Site
{
	[Area("Admin")]
    [PermissionChecker(Shared.Domain.Enum.UserPermission.مدیریت_سایت)]
    public class MenuController : Controller
	{
		private readonly IMenuQuery _MenuQuery;
		private readonly IMenuApplication _MenuApplication;
		private readonly IAuthService _authService;
		public MenuController(IMenuQuery MenuQuery, IMenuApplication MenuApplication, IAuthService
			authService)
		{
			_MenuQuery = MenuQuery;
			_MenuApplication = MenuApplication;
			_authService = authService;
		}
		public IActionResult Index(int id = 0) => View(_MenuQuery.GetForAdmin(id));
		public IActionResult Create() => View();
		[HttpPost]
		public IActionResult Create( CreateMenu model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _MenuApplication.Create(model);
			if (res.Success)
			{
				TempData["ok"] = true;
                return Redirect("/Admin/Menu/Index/0");
            }
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}
		public IActionResult CreateSub(int id) => View(_MenuApplication.GetForCreate(id));
		[HttpPost]
        public IActionResult CreateSub(int id,CreateSubMenu model)
		{
            if (!ModelState.IsValid) return View(model);
            var res = _MenuApplication.CreateSub(model);
            if (res.Success)
            {
                TempData["ok"] = true;
                return Redirect($"/Admin/Menu/Index/{id}");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }

        public IActionResult Edit(int id)
		{
			var model = _MenuApplication.GetForEdit(id);
			return View(model);
		}
		[HttpPost]
		public IActionResult Edit(int id, EditMenu model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _MenuApplication.Edit(model);
			if (res.Success)
			{
				TempData["ok"] = true;
                return Redirect($"/Admin/Menu/Index/{model.ParentId}");
            }
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}
		public bool Active(int id) => _MenuApplication.ActivationChange(id);
	}
}

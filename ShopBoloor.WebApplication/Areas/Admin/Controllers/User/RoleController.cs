using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enum;
using ShopBoloor.WebApplication.Utility;
using Users.Application.Contract.RoleApplication.Command;
using Users.Application.Contract.RoleApplication.Query;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.User
{
    [Area("Admin")]
    [PermissionChecker(UserPermission.مدیریت_نقش_ها)]
    public class RoleController : Controller
    {
        private readonly IRoleQuery _roleQuery;
        private readonly IRoleApplication _roleApplication;
        public RoleController(IRoleQuery roleQuery, IRoleApplication roleApplication)
        {
            _roleQuery = roleQuery;
            _roleApplication = roleApplication; 
        }
        public IActionResult Index() => View(_roleQuery.GetAllRoles());
        public IActionResult Create()=> View();
        [HttpPost]
        public IActionResult Create(CreateRole model,List<UserPermission> permissions)
        {
            if (!ModelState.IsValid) return View(model);
            var res = _roleApplication.Create(model, permissions);
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
            var model = _roleApplication.GetForEdit(id);
            ViewData["permissions"] = _roleApplication.GetPermissionsForRole(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id,EditRole model, List<UserPermission> permissions)
        {
            ViewData["permissions"] = _roleApplication.GetPermissionsForRole(id);
            if (!ModelState.IsValid) return View(model);
            var res = _roleApplication.Edit(model,permissions);
            if (res.Success)
            {
                TempData["ok"] = true;
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
    }
}

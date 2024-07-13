using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.User;
using Users.Application.Contract.UserApplication.Command;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.User
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IAdminUserQuery _adminUserQuery;
        private readonly IUserApplication _userApplication;
        public UserController(IAdminUserQuery adminUserQuery, IUserApplication userApplication)
        {
            _adminUserQuery = adminUserQuery;
            _userApplication = userApplication;
        }
        public IActionResult Index(int pageId = 1 ,int take = 20,string filter = "" , 
            UserOrderBySearch orderBy = UserOrderBySearch.تاریخ_ثبت_از_آخر_به_اول,
            UserStatusSearch status = UserStatusSearch.همه)
        {
            var model = _adminUserQuery.GetUsersForAdmin(pageId, filter, take, orderBy, status);
            return View(model);
        }
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(CreateUser model)
        {
            if (!ModelState.IsValid) return View(model);
            var result = _userApplication.Create(model);
            if (result.Success)
            {
                TempData["ok"] = true;
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(result.ModelName, result.Message);
            return View(model);
        }
        public IActionResult Edit(int id) =>View(_userApplication.GetForEditByAdmin(id));
        [HttpPost]
        public IActionResult Edit(int id,EditUserByAdmin model)
        {
            if (!ModelState.IsValid) return View(model);
            var result = _userApplication.Edit(model);
            if (result.Success)
            {
                TempData["ok"] = true;
                return Redirect($"/Admin/User/Edit/{id}");
            }
            ModelState.AddModelError(result.ModelName, result.Message);
            return View(model);
        }
        public bool Active(int id) => _userApplication.ActivationChange(id);
        public bool Delete(int id) => _userApplication.DeleteChange(id);
    }
}

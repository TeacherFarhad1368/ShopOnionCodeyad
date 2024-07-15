using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.User;
using Shared.Application;
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
        public IActionResult Create() => PartialView("Create",new CreateUser());
        [HttpPost]
        public JsonResult Create(CreateUser model)
        {
            OperationResult res = _userApplication.Create(model);
            return new JsonResult(res);
        }
        public IActionResult Edit(int id) =>PartialView("Edit",_userApplication.GetForEditByAdmin(id));
        [HttpPost]
        public JsonResult Edit(int id,EditUserByAdmin model)
        {
            if (!ModelState.IsValid) {
                OperationResult res = new(false, "اطلاعات را درست وارد کنید .");
                return new JsonResult(res);
            };
            var result = _userApplication.Edit(model);
            return new JsonResult(result);
        }
        public bool Active(int id) => _userApplication.ActivationChange(id);
        public bool Delete(int id) => _userApplication.DeleteChange(id);
    }
}

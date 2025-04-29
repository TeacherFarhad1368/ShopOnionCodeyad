using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.Seo;
using Seos.Application.Contract;
using Shared.Domain.Enum;
using ShopBoloor.WebApplication.Utility;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Seo
{
    [Area("Admin")]
    [PermissionChecker(Shared.Domain.Enum.UserPermission.مدیریت_Seo)]
    public class SeoController : Controller
    {
        private readonly ISeoApplication _seoApplication;
        private readonly ISeoAdminQuery _seoAdminQuery;

        public SeoController(ISeoApplication seoApplication, ISeoAdminQuery seoAdminQuery)
        {
            _seoApplication = seoApplication;
            _seoAdminQuery = seoAdminQuery;
        }
        [Route("/Admin/Seo/{id}/{where}")]
        public IActionResult Index(int id,WhereSeo where)
        {
            ViewData["Title"] = _seoAdminQuery.GetAdminSeoTitle(where, id);
            var model = _seoApplication.GetSeoForEdit(id, where);
            return View(model);
        }
        [HttpPost]
        [Route("/Admin/Seo/{id}/{where}")]
        public IActionResult Index(int id, WhereSeo where,CreateSeo model)
        {
            ViewData["Title"] = _seoAdminQuery.GetAdminSeoTitle(where, id);
            if (!ModelState.IsValid) return View(model);
            var res = _seoApplication.UbsertSeo(model);
            if (res)
            {
                TempData["ok"] = true;
                return Redirect($"/Admin/Seo/{id}/{where}");
            }
            else
            {
                TempData["faild"] = true;
                return Redirect($"/Admin/Seo/{id}/{where}");
            }
        }
    }
}

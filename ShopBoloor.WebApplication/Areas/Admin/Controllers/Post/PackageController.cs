using Microsoft.AspNetCore.Mvc;
using PostModule.Application.Contract.UserPostApplication.Command;
using PostModule.Application.Contract.UserPostApplication.Query;
using System.Diagnostics.Contracts;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Package
{
    [Area("Admin")]
    public class PackageController : Controller
    {
        private readonly IPackageQuery _PackageQuery;
        private readonly IPackageApplication _PackageApplication;

        public PackageController(IPackageQuery PackageQuery, IPackageApplication PackageApplication)
        {
            _PackageQuery = PackageQuery;
            _PackageApplication = PackageApplication;
        }

        public IActionResult Index()
        {
            return View(_PackageQuery.GetAll());
        }
        public IActionResult Create() => View();
        [HttpPost] 
        public IActionResult Create(CreatePackage model)
        {
            if (!ModelState.IsValid) return View(model);
            var res = _PackageApplication.Create(model);
            if(res.Success)
            {
                TempData["ok"] = true;
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
        public IActionResult Edit(int id) => View(_PackageApplication.GetForEdit(id));
        [HttpPost]
        public IActionResult Edit(int id,EditPackage model)
        {
            if (!ModelState.IsValid) return View(model);
            var res = _PackageApplication.Edit(model);
            if (res.Success)
            {
                TempData["ok"] = true;
                return Redirect($"/Admin/Package/Edit/{id}");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
        public bool Active(int id) => _PackageApplication.ActivationChange(id);
    }
}

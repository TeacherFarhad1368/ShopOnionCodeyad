using Microsoft.AspNetCore.Mvc;
using Query.Contract.UI.PostPackage;

namespace ShopBoloor.WebApplication.Controllers
{
    public class PostController : Controller
    {
        private readonly IPackageUiQuery _packageUiQuery;
        public PostController(IPackageUiQuery packageUiQuery)
        {
            _packageUiQuery = packageUiQuery;
        }
        public IActionResult Index()
        {
            var model = _packageUiQuery.GetPackagesForUi();
            return View(model);
        }
    }
}

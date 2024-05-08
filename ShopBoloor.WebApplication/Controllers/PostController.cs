using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostModule.Application.Contract.StateQuery;
using Query.Contract.UI.PostPackage;

namespace ShopBoloor.WebApplication.Controllers
{
    public class PostController : Controller
    {
        private readonly IPackageUiQuery _packageUiQuery;
        private readonly IStateQuery _stateQuery;
        public PostController(IPackageUiQuery packageUiQuery, IStateQuery stateQuery)
        {
            _packageUiQuery = packageUiQuery;
            _stateQuery = stateQuery;
        }
        public IActionResult Index()
        {
            var model = _packageUiQuery.GetPackagesForUi();
            return View(model);
        }
        public JsonResult GetStates()
        {
            var model = _stateQuery.GetStatesForChoose();
            var json = JsonConvert.SerializeObject(model);
            return Json(json);
        }
        public JsonResult GetCities(int id)
        {
            var model = _stateQuery.GetCitiesForChoose(id);
            var json = JsonConvert.SerializeObject(model);
            return Json(json);
        }
    }
}

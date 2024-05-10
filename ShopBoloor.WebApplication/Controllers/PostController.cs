using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostModule.Application.Contract.PostCalculate;
using PostModule.Application.Contract.StateQuery;
using Query.Contract.UI.PostPackage;

namespace ShopBoloor.WebApplication.Controllers
{
    public class PostController : Controller
    {
        private readonly IPackageUiQuery _packageUiQuery;
        private readonly IStateQuery _stateQuery;
        private readonly IPostCalculateApplication _postCalculateApplication;
        public PostController(IPackageUiQuery packageUiQuery, IStateQuery stateQuery, IPostCalculateApplication postCalculateApplication)
        {
            _packageUiQuery = packageUiQuery;
            _stateQuery = stateQuery;
            _postCalculateApplication = postCalculateApplication;   
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
        [HttpPost]
        public async Task<JsonResult> Calculate(int sourceId,int destinationId,int weight)
        {
            PostPriceRequestModel req = new PostPriceRequestModel()
            {
                DestinationCityId = destinationId,
                SourceCityId = sourceId,
                Weight = weight
            };
            var calcuLate =await _postCalculateApplication.CalculatePost(req);
            var model = JsonConvert.SerializeObject(calcuLate);
            return Json(model);
        }
    }
}

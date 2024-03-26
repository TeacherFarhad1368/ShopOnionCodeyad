using Microsoft.AspNetCore.Mvc;
using PostModule.Application.Contract.CityApplication;
using PostModule.Application.Contract.StateApplication;
using PostModule.Application.Contract.StateQuery;
using Shared.Domain.Enum;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Post
{
	[Area("Admin")]
	public class CityController : Controller
	{
		private readonly IStateQuery _stateQuery;
		private readonly ICityApplication _cityApplication;
		private readonly IStateApplication _stateApplication;

        public CityController(IStateQuery stateQuery, ICityApplication cityApplication, IStateApplication stateApplication)
        {
            _stateQuery = stateQuery;
            _cityApplication = cityApplication;
            _stateApplication = stateApplication;
        }

        public IActionResult Index(int id)
		{
			return View( _stateQuery.GetStateDetail(id));
		}
		public IActionResult Create() => View();
		[HttpPost]
		public IActionResult Create(CreateCityModel model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _cityApplication.Create(model);
			if (res.Success)
			{
				TempData["ok"] = true;
				return Redirect($"/Admin/City/Index/{model.StateId}");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}
		public IActionResult Edit(int id)
		{
			var model = _stateApplication.GetStateForEdit(id);
			return View(model);
		}
		[HttpPost]
		public IActionResult Edit(int id, EditCityModel model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _cityApplication.Edit(model);
			if (res.Success)
			{
				TempData["ok"] = true;
				return RedirectToAction("Imdex");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}
		public bool Status(int id,CityStatus status) => _cityApplication.ChangeStatus(id, status);
	}
}

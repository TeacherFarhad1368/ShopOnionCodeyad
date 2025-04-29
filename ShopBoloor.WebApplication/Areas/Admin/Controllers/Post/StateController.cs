using Emails.Application.Contract.SensEmailApplication.Command;
using Microsoft.AspNetCore.Mvc;
using PostModule.Application.Contract.StateApplication;
using PostModule.Application.Contract.StateQuery;
using ShopBoloor.WebApplication.Utility;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Post
{
	[Area("Admin")]
    [PermissionChecker(Shared.Domain.Enum.UserPermission.مدیریت_پست)]
    public class StateController : Controller
	{
		private readonly IStateApplication _stateApplication;
		private readonly IStateQuery _stateQuery;

		public StateController(IStateApplication stateApplication, IStateQuery stateQuery)
		{
			_stateApplication = stateApplication;
			_stateQuery = stateQuery;
		}

		public IActionResult Index()
		{
			return View(_stateQuery.GetStatesForAdmin());
		}
		public IActionResult Create() => View();
		[HttpPost]
		public IActionResult Create(CreateStateModel model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _stateApplication.Create(model);
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
			var model = _stateApplication.GetStateForEdit(id);	
			return View(model);	
		}
		[HttpPost]
		public IActionResult Edit(int id,EditStateModel model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _stateApplication.Edit(model);
			if (res.Success)
			{
				TempData["ok"] = true;
				return RedirectToAction("Index");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}
		public IActionResult ChangeClose(int id,List<int>? stateCloses)
		{
			if(stateCloses.Count() < 1)
			{
				TempData["ChooseState"] = true;
				return Redirect($"/Admin/City/Index/{id}");
			}
			if(_stateApplication.ChangeStateClose(id,stateCloses))
			{
				TempData["ok"] = true;
				return Redirect($"/Admin/City/Index/{id}");
			}
			else
			{
                TempData["faild"] = true;
				return Redirect($"/Admin/City/Index/{id}");
			}
		}
    }
}

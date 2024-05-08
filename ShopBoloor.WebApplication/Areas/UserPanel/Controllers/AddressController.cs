using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PostModule.Application.Contract.StateQuery;
using Query.Contract.UserPanel.Address;
using Shared.Application.Services.Auth;
using Users.Application.Contract.UserAddressApplication.Command;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class AddressController : Controller
    {
        private int _userId;
        private readonly IUserAddressUserPanelQuery _userAddressUserPanelQuery;
        private readonly IAuthService _authService;
        private readonly IUserAddressApplication _userAddressApplication;
        private readonly IStateQuery _stateQuery;
        public AddressController(IUserAddressUserPanelQuery userAddressUserPanelQuery,IAuthService authService,
            IUserAddressApplication userAddressApplication, IStateQuery stateQuery)
        {
            _authService = authService;
            _userAddressApplication = userAddressApplication;
            _userAddressUserPanelQuery = userAddressUserPanelQuery;
            _stateQuery = stateQuery;
        }
        public IActionResult Index()
        {
            _userId = _authService.GetLoginUserId();
            var model = _userAddressUserPanelQuery.GetAddresseForUserPanel(_userId);
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateAddress model)
        {
            if (!ModelState.IsValid) return View(model);
            if(_stateQuery.IsStateCorrect(model.StateId) == false)
            {
                ModelState.AddModelError(nameof(model.StateId), "لطفا یک استان انتخاب کنید");
                return View(model);
            }
            if (_stateQuery.IsCityCorrect(model.StateId,model.CityId) == false)
            {
                ModelState.AddModelError(nameof(model.CityId), "لطفا یک شهر انتخاب کنید");
                return View(model);
            }
            _userId = _authService.GetLoginUserId();
            var res = _userAddressApplication.Create(model, _userId);
            if (res.Success)
            {
                TempData["ok"] = true;
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(res.ModelName,res.Message);
            return View(model);
        }
        public bool Delete(int id)
        {
            _userId = _authService.GetLoginUserId();
            return _userAddressApplication.Delete(id,_userId);
        }
    }
}

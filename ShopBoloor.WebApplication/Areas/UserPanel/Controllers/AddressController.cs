using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public AddressController(IUserAddressUserPanelQuery userAddressUserPanelQuery,IAuthService authService,
            IUserAddressApplication userAddressApplication)
        {
            _authService = authService;
            _userAddressApplication = userAddressApplication;
            _userAddressUserPanelQuery = userAddressUserPanelQuery;
        }
        public IActionResult Index()
        {
            _userId = _authService.GetLoginUserId();
            var model = _userAddressUserPanelQuery.GetAddresseForUserPanel(_userId);
            return View(model);
        }
    }
}

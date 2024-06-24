using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.UserPanel.Wallet;
using Shared.Application.Services.Auth;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class WalletController : Controller
    {
        private int _userId;
        private readonly IAuthService _authService;
        private readonly IUserPanelWalletQuery _userPanelWalletQuery;
        public WalletController(IAuthService authService,IUserPanelWalletQuery userPanelWalletQuery)
        {
            _authService = authService;
            _userPanelWalletQuery = userPanelWalletQuery;   
        }
        public IActionResult Index(int pageId,string filter)
        {
            _userId = _authService.GetLoginUserId();
            var model = _userPanelWalletQuery.GetWalletsForUserPanel(_userId,pageId,filter);    
            return View(model);
        }
        public IActionResult Transactions(int pageId, string filter)
        {
            _userId = _authService.GetLoginUserId();
            var model = _userPanelWalletQuery.GetTransactionsForUserPanel(_userId, pageId, filter);
            return View(model);
        }

    }
}

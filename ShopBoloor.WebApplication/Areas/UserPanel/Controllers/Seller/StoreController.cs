using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.UserPanel.Store;
using Shared.Application.Services.Auth;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers.Seller
{
    [Area("UserPanel")]
    [Authorize]
    public class StoreController : Controller
    {
        private int _userId;
        private readonly IAuthService _authService;
        private readonly IStoreUserPanelQuery _storeUserPanelQuery;
        public StoreController(IAuthService authService, IStoreUserPanelQuery storeUserPanelQuery)
        {
            _authService = authService;
            _storeUserPanelQuery = storeUserPanelQuery; 
        }

        public IActionResult Index(int pageId = 1,int sellerId = 0,int take = 10,string filter ="")
        {
            _userId = _authService.GetLoginUserId();
            var model = _storeUserPanelQuery.GetStoresForUserPanel(_userId, sellerId, filter, pageId, take);
            if (model == null) return NotFound();
            return View(model);
        }
    }
}

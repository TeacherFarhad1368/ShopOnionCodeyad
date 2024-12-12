using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Query.Contract.UserPanel.Store;
using Shared.Application.Services.Auth;
using Stores.Application.Contract.StoreApplication.Command;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers.Seller
{
    [Area("UserPanel")]
    [Authorize]
    public class StoreController : Controller
    {
        private int _userId;
        private readonly IAuthService _authService;
        private readonly IStoreUserPanelQuery _storeUserPanelQuery;
        private readonly IStoreApplication _storeApplication;
        public StoreController(IAuthService authService, IStoreUserPanelQuery storeUserPanelQuery,
            IStoreApplication storeApplication)
        {
            _authService = authService;
            _storeUserPanelQuery = storeUserPanelQuery; 
            _storeApplication = storeApplication;
        }

        public IActionResult Index(int pageId = 1,int sellerId = 0,int take = 10,string filter ="")
        {
            _userId = _authService.GetLoginUserId();
            var model = _storeUserPanelQuery.GetStoresForUserPanel(_userId, sellerId, filter, pageId, take);
            if (model == null) return NotFound();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        public JsonResult GetSellers()
        {
            _userId = _authService.GetLoginUserId();
            var res = _storeUserPanelQuery.GetSellersForCreateStore(_userId);
            return Json(JsonConvert.SerializeObject(res));
        }
        public JsonResult GetSellerProducts(int id)
        {
            _userId = _authService.GetLoginUserId();
            var res = _storeUserPanelQuery.GetSellerProductsForCreateStore(id,_userId);
            return Json(JsonConvert.SerializeObject(res));
        }
        [HttpPost]
        public async Task<bool> Create( string model)
        {
            _userId = _authService.GetLoginUserId();
            CreateStore res = JsonConvert.DeserializeObject<CreateStore>(model);
            if(_storeUserPanelQuery.CheckCreateStore(res, _userId) == false)
            {
                return false;
            }
            var result = await _storeApplication.CreateAsync(_userId,res);
            if(result.Success) return true;
            else return false;  
        }
        public IActionResult Detail(int id)
        {
            _userId = _authService.GetLoginUserId();
            var model = _storeUserPanelQuery.GetStoreDetailForSellerPanel(_userId, id);
            if (model == null) return NotFound();
            return View(model);
        }
    }
}

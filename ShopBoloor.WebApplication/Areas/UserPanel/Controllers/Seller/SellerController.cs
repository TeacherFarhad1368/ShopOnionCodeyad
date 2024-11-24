using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostModule.Application.Contract.StateQuery;
using Query.Contract.UserPanel.Seller;
using Shared.Application.Services.Auth;
using Shop.Application.Contract.SellerApplication.Command;
namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers.Seller;
[Area("UserPanel")]
[Authorize]
public class SellerController : Controller
{
    private readonly ISellerUserPanelQuery _sellerUserPanelQuery;
    private readonly IAuthService _authService;
    private readonly ISellerApplication _sellerApplication;
    private readonly IStateQuery _stateQuery;
    private int _userId;
    public SellerController(ISellerUserPanelQuery sellerUserPanelQuery, IAuthService authService, 
        ISellerApplication sellerApplication, IStateQuery stateQuery)
    {
        _sellerUserPanelQuery = sellerUserPanelQuery;
        _authService = authService;
        _sellerApplication = sellerApplication;
        _stateQuery = stateQuery;   
    }

    public IActionResult Index()
    {
        _userId = _authService.GetLoginUserId();
        var model = _sellerUserPanelQuery.GetSellersForUser(_userId);
        return View(model);
    }
    public IActionResult Request() => View();
    [HttpPost]
    public async Task<IActionResult> Request(RequestSeller model)
    {
        _userId = _authService.GetLoginUserId();
        if (!ModelState.IsValid) return View(model);
        if (_stateQuery.IsCityCorrect(model.StateId, model.CityId) == false)
        {
            ModelState.AddModelError("StateId", "لطفا استان و شهر فروشگاه خود را صحیح وارد کنید .");
            return View(model);
        }
        var res = await _sellerApplication.RequestSellerAsync(_userId, model);
        if (res.Success)
        {
            TempData["ok"] = true;
            return RedirectToAction("Index");
        }
        ModelState.AddModelError(res.ModelName, res.Message);
        return View(model);
    }
    public async Task<IActionResult> Edit(int id)
    {
        _userId = _authService.GetLoginUserId();
        var model = await _sellerApplication.GetRequsetFoeEditAsync(id, _userId);
        if (model == null) return NotFound();
        return View(model); 
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id,EditSellerRequest model)
    {
        _userId = _authService.GetLoginUserId(); 
        if (!ModelState.IsValid) return View(model);
        if (_stateQuery.IsCityCorrect(model.StateId, model.CityId) == false)
        {
            ModelState.AddModelError("StateId", "لطفا استان و شهر فروشگاه خود را صحیح وارد کنید .");
            return View(model);
        }
        var res = await _sellerApplication.EditRequestSellerAsync(_userId, model);
        if (res.Success)
        {
            TempData["ok"] = true;
            return RedirectToAction("Index");
        }
        ModelState.AddModelError(res.ModelName, res.Message);
        return View(model);
    }
    public IActionResult Detail(int id)
    {
        _userId = _authService.GetLoginUserId();
        var model = _sellerUserPanelQuery.GetSellerDetailForSeller(id, _userId);
        if(model == null) return NotFound();
        return View(model); 
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.UserPanel.Seller;
using Shared.Application.Services.Auth;
namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers.Seller;

[Area("UserPanel")]
[Authorize]
public class ProductController : Controller
{
    private readonly ISellerUserPanelQuery _sellerUserPanelQuery;
    private readonly IAuthService _authService;
    private int _userId;

    public ProductController(ISellerUserPanelQuery sellerUserPanelQuery, IAuthService authService)
    {
        _sellerUserPanelQuery = sellerUserPanelQuery;
        _authService = authService;
    }
    public IActionResult Index(int id,int pageId = 1,string filter = "")
    {
        _userId = _authService.GetLoginUserId();
        var model = _sellerUserPanelQuery.GetSellerProductsForUserPanel(pageId,filter,id,_userId);
        if (model == null) return NotFound();
        return View(model); 
    }
}

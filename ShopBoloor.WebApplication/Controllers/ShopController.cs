using Microsoft.AspNetCore.Mvc;
using Query.Contract.UI.Product;
using Shared.Application.Services.Auth;
using Shop.Domain.ProductAgg;
using System.Collections.Generic;
using System.Text.Json;

namespace ShopBoloor.WebApplication.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductUiQuery _productUiQuery;
        private readonly IAuthService _authService;
        public ShopController(IProductUiQuery productUiQuery, IAuthService authService)
        {
            _productUiQuery = productUiQuery;
            _authService = authService;
        }

        [Route("/Shop/{id?}")]
        public IActionResult Index(int? id,string slug = "",int pageId = 1,string filter="",ShopOrderBy orderBy = ShopOrderBy.جدید_ترین)
        {
            var model = _productUiQuery.GetProductsForUi(pageId,filter,slug,id == null ? 0 : id.Value,orderBy);
            return View(model);
        }
        [Route("/Product/{id}/{slug}")]
        public IActionResult Single(int id,string slug)
        {
            var model = _productUiQuery.GetSingleProductForUi(id);
            if (model == null) return NotFound();
            if (model.Slug != slug) return Redirect($"/Product/{id}/{model.Slug}");
            return View(model);
        }
        [Route("/Cart")]
        public IActionResult Cart()
        {
            if (_authService.IsUserLogin())
                return Redirect("/UserPanel/Order/Order");
            return View();
        }
    }
}

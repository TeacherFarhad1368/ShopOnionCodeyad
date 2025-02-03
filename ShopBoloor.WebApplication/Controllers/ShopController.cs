using Microsoft.AspNetCore.Mvc;
using Query.Contract.UI.Product;
using Shop.Domain.ProductAgg;
using System.Collections.Generic;
using System.Text.Json;

namespace ShopBoloor.WebApplication.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductUiQuery _productUiQuery;

        public ShopController(IProductUiQuery productUiQuery)
        {
            _productUiQuery = productUiQuery;
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
            return View();
        }
    }
}

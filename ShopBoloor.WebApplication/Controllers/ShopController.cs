using Microsoft.AspNetCore.Mvc;
using Query.Contract.UI.Product;

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
        public IActionResult Index(int? id,string slug = "",int pageId = 0,string filter="",ShopOrderBy orderBy = ShopOrderBy.جدید_ترین)
        {
            var model = _productUiQuery.GetProductsForUi(pageId,filter,slug,id,orderBy);
            return View(model);
        }
    }
}

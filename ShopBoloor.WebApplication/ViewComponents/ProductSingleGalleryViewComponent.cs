using Microsoft.AspNetCore.Mvc;
using Query.Contract.UI.Product;
using Site.Application.Contract.SiteSettingApplication.Query;

namespace ShopBoloor.WebApplication.ViewComponents
{
    public class ProductSingleGalleryViewComponent : ViewComponent
    {
        private readonly IProductUiQuery _query;
        public ProductSingleGalleryViewComponent(IProductUiQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke(int productId)
        {
            var model = _query.GetProductSingleGallery(productId);
            return View(model);
        }
    }
}

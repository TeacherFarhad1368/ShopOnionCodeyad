using Shop.Application.Contract.ProductFeautreApplication.Query;
using Shop.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Services
{
    internal class ProductFeatureQuery : IProductFeautreQuery
    {
        private readonly IProductFeatureRepository _productFeatureRepository;
        private readonly IProductRepository _productRepository;
        public ProductFeatureQuery(IProductFeatureRepository productFeatureRepository, IProductRepository productRepository)
        {
            _productFeatureRepository = productFeatureRepository;
            _productRepository = productRepository;
        }

        public ProductFeaturePageAdminQueryModel GetProductFeaturesForAdmin(int productId)
        {
            ProductFeaturePageAdminQueryModel model = new ProductFeaturePageAdminQueryModel();
            var product = _productRepository.GetById(productId);
            List<ProductFeatureAdminQueryModel> Features = new();
            var res = _productFeatureRepository.GetAllByQuery(c=>c.ProductId ==  productId);
            Features =  res.Select(r => new ProductFeatureAdminQueryModel(r.Id,r.Title,r.Value)).ToList();
            return new ProductFeaturePageAdminQueryModel()
            {
                Feautures = Features,
                ProductId = productId,
                Title = $"لیست ویژگی های  {product.Title}"
            };
        }
    }
}

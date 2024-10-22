using Shop.Application.Contract.ProductGalleryApplication.Query;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductGalleryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Services
{
    internal class ProductGalleryQuery : IProductGalleryQuery
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductGalleryRepository _productGalleryRepository;

        public ProductGalleryQuery(IProductRepository productRepository, IProductGalleryRepository productGalleryRepository)
        {
            _productRepository = productRepository;
            _productGalleryRepository = productGalleryRepository;
        }

        public ProductGalleryAdminPageQueryModel GetProductGalleriesForAdmin(int productId)
        {
            var product = _productRepository.GetById(productId);
            var res = _productGalleryRepository.GetAllByQuery(c=>c.ProductId == productId);
            return new ProductGalleryAdminPageQueryModel()
            {
                Galleries = res.Select(r=> new ProductGalleryAdminQueryModel(r.Id,r.ImageName,r.ImageAlt)).ToList(),
                ProductId = productId,
                Title = $"گالری تصاویر {product.Title}"
            };
        }
    }
}

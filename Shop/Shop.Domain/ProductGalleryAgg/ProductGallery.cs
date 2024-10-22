using Shared.Domain;
using Shop.Domain.ProductAgg;

namespace Shop.Domain.ProductGalleryAgg
{
    public class ProductGallery : BaseEntityCreate<int>
    {
        public int ProductId { get; private set; }
        public string ImageName { get; private set; }
        public string ImageAlt { get; private set; }
        public Product Product { get; private set; }
        public ProductGallery()
        {
            Product = new();
        }

        public ProductGallery(int productId, string imageName, string imageAlt)
        {
            ProductId = productId;
            ImageName = imageName;
            ImageAlt = imageAlt;
        }
    }
}

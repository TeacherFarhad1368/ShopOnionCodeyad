using Shared.Domain;
using Shop.Domain.ProductCategoryRelationAgg;
using Shop.Domain.ProductGalleryAgg;
using Shop.Domain.ProductSellAgg;
namespace Shop.Domain.ProductAgg
{
    public class Product : BaseEntityCreateUpdateActive<int>
    {
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string ImageName { get; private set; }
        public string ImageAlt { get; private set; }
        public int Weight { get; private set; }
        public List<ProductCategoryRelation> ProductCategoryRelations { get; private set; }
        public List<ProductFeature> ProductFeatures { get; private set; }
        public List<ProductGallery> ProductGalleries { get; private set; }
        public List<ProductSell> ProductSells { get; private set; }
        public void EditCategoryRelations(List<ProductCategoryRelation> categoryRelations)
        {
            ProductCategoryRelations = categoryRelations;   
        }
        public Product()
        {
            ProductCategoryRelations = new();
            ProductFeatures = new();
            ProductGalleries = new();
            ProductSells = new();
        }

        public void Edit(string title, string slug, string shortDescription,
            string description, string imageName, string imageAlt, int weight)
        {
            Title = title;
            Slug = slug;
            ShortDescription = shortDescription;
            Description = description;
            ImageName = imageName;
            ImageAlt = imageAlt;
            Weight = weight;
        }
        public Product(string title, string slug, string shortDescription, 
            string description, string imageName, string imageAlt, int weight)
        {
            Title = title;
            Slug = slug;
            ShortDescription = shortDescription;
            Description = description;
            ImageName = imageName;
            ImageAlt = imageAlt;
            Weight = weight;
        }
    }
}

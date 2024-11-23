using Shared.Domain;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductCategoryAgg;

namespace Shop.Domain.ProductCategoryRelationAgg
{
    public class ProductCategoryRelation : BaseEntity<int>
    {
        public int ProductId { get; internal set; }
        public int ProductCategoryId { get; private set; }
        public Product Product { get; private set; }
        public ProductCategory ProductCategory { get; private set; }
        public ProductCategoryRelation()
        {
            Product = new();
            ProductCategory = new();
        }
        public ProductCategoryRelation(int productCategoryId)
        {
            ProductCategoryId = productCategoryId;
        }
        public ProductCategoryRelation(int productId, int productCategoryId)
        {
            ProductId = productId;
            ProductCategoryId = productCategoryId;
        }
    }
}

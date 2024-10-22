using Shared.Domain;

namespace Shop.Domain.ProductAgg
{
    public class ProductFeature : BaseEntity<int>
    {
        public int ProductId { get; private set; }
        public string Title { get; private set; }
        public string Value { get; private set; }
        public Product Product { get; private set; }
        public ProductFeature()
        {
            Product = new();
        }

        public ProductFeature(int productId, string title, string value)
        {
            ProductId = productId;
            Title = title;
            Value = value;
        }
    }
}

namespace Shop.Application.Contract.ProductFeautreApplication.Query
{
    public class ProductFeaturePageAdminQueryModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public List<ProductFeatureAdminQueryModel> Feautures { get; set; }
    }
}

namespace Shop.Application.Contract.ProductFeautreApplication.Query
{
    public class ProductFeatureAdminQueryModel
    {
        public ProductFeatureAdminQueryModel(int id, string title, string value)
        {
            Id = id;
            Title = title;
            Value = value;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Value { get; private set; }
    }
}

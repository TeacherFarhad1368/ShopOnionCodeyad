namespace Shop.Application.Contract.ProductCategoryApplication.Query
{
    public class ProductCategoryAdminPageQueryModel
    {
        public ProductCategoryAdminPageQueryModel(int id, string pageTitle, List<ProductCategoryAdminQueryModel> categories)
        {
            Id = id;
            PageTitle = pageTitle;
            Categories = categories;
        }

        public int Id { get; private set; }
        public string PageTitle { get; private set; }
        public List<ProductCategoryAdminQueryModel> Categories { get; private set; }
    }
}

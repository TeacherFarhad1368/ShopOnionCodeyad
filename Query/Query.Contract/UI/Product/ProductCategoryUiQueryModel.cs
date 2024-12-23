namespace Query.Contract.UI.Product;

public class ProductCategoryUiQueryModel
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public List<ProductCategoryUiQueryModel> Childs { get; set; }
}

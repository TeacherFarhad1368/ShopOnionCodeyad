namespace Query.Contract.UI.Product;

public class SingleProductUIQueryModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string ImageName { get; set; }
    public string ImageAlt { get; set; }
    public string Description { get; set; }
    public int Weight { get; set; }
    public List<FeatureForProductSingleQueryModel> Features { get; set; }
    public List<CategoryForProductSingleQueryModel> Categories { get; set; }
    public List<ProductSellForProductSingleQueryModel> ProductSells { get; set; }
    public List<BreadCrumbQueryModel> BreadCrumb { get; set; }
}

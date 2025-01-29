namespace Query.Contract.UI.Product;
public interface IProductUiQuery
{
    ShopPaging GetProductsForUi(int pageId, string filter, string categorySlug, int Id, ShopOrderBy orderBy);
    SingleProductUIQueryModel GetSingleProductForUi(int id);
}
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
    public List<GalleryForProductSingleQueryModel> Galleries { get; set; }
    public List<CategoryForProductSingleQueryModel> Categories { get; set; }
    public List<ProductSellForProductSingleQueryModel> ProductSells { get; set; }
    public List<BreadCrumbQueryModel> BreadCrumb { get; set; }
    public SeoUiQueryModel Seo { get; set; }
}
public class FeatureForProductSingleQueryModel
{
    public string Title { get; set; }
    public string Value { get; set; }
}
public class GalleryForProductSingleQueryModel
{
    public string ImageName { get; set; }
    public string ImageAlt { get; set; }
}
public class CategoryForProductSingleQueryModel
{
    public string Title { get; set; }
    public string Slug { get; set; }
}
public class ProductSellForProductSingleQueryModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int SellerId { get; set; }
    public int Price { get; set; }
    public int PriceAfterOff { get; set; }
    public int Amount { get; set; }
    public string Unit { get; set; }
    public int Weight { get; set; }
    public string SellerName { get; set; }
    public string SellerAddress { get; set; }
}
using Shared;

namespace Query.Contract.UI.Product;
public interface IProductUiQuery
{
    ShopPaging GetProductsForUi(int pageId, string filter, string categorySlug, int Id, ShopOrderBy orderBy);
}
public class ShopPaging : BasePaging
{
    public int ShopId { get; set; }
    public string ShopTitle { get; set; }
    public string CategorySlug { get; set; }
    public string Filter { get; set; }
    public ShopOrderBy OrderBy { get; set; }
    public List<ProductShopUiQueryModel> Products { get; set; }
}
public class ProductShopUiQueryModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string ImageName { get; set; }
    public string ImageAlt { get; set; }
    public string Shop { get; set; }
    public int Price { get; set; }
    public int PriceAfterOff { get; set; }
}
public enum ShopOrderBy
{
    جدید_ترین,
    قدیمی_ترین,
    پرفروش_ترین,
    ارزان_ترین,
    گران_ترین,
}
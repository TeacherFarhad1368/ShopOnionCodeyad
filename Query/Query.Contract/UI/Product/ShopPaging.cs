using Shared;

namespace Query.Contract.UI.Product;

public class ShopPaging : BasePaging 
{
    public int ShopId { get; set; }
    public string ShopTitle { get; set; }
    public string CategorySlug { get; set; }
    public string Filter { get; set; }
    public ShopOrderBy OrderBy { get; set; }
    public List<ProductShopUiQueryModel> Products { get; set; }
    public List<ProductCategoryUiQueryModel> Categories { get; set; }
    public List<BreadCrumbQueryModel> BreadCrumb { get; set; }
    public SeoUiQueryModel Seo { get; set; }
}

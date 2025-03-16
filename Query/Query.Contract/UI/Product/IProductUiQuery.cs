namespace Query.Contract.UI.Product;
public interface IProductUiQuery 
{
    List<ProductCartForIndexQueryModel> GetBestPeoductSellForIndex();
    List<ProductCartForIndexQueryModel> GetNewPeoductForIndex();
    List<ProductCartForIndexQueryModel> GetBestPeoductVisitForIndex();
    ShopPaging GetProductsForUi(int pageId, string filter, string categorySlug, int Id, ShopOrderBy orderBy);
    SingleProductUIQueryModel GetSingleProductForUi(int id);
    List<WishListProductQueryModel> GetWishListForUserLoggedIn(int userId);
    List<WishListProductQueryModel> GetWishListForUserFromCppkie(List<int> productIds);
    List<AmazingSliderQueryModel> GetAmazingSliderData();
    List<WishListForUserPanelQueryModel> GetLastWishListForUserPanel(int userId);
    List<AjaxSearchModel> SearchAjax(string filter);
}
public class AjaxSearchModel
{
    public string ImageAddress { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public int id { get; set; }
}

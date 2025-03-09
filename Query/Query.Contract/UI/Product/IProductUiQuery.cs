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
}

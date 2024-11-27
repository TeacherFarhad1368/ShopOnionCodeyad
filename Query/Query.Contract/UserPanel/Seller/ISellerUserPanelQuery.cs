namespace Query.Contract.UserPanel.Seller;
public interface ISellerUserPanelQuery
{
    SellerDetailForUserPanelQueryModel GetSellerDetailForSeller(int id, int userId);
    SellerProductPageUserPanelQueryModel GetSellerProductsForUserPanel(int pageId,string filter,int sellerId, int userId);
    List<SellersUserPanelQueryModel> GetSellersForUser(int userId);
    bool IsSellerForUser(int id, int userId);
}


using Shared.Domain.Enum;

namespace Query.Contract.UserPanel.Seller;
public interface ISellerUserPanelQuery
{
    SellerDetailForUserPanelQueryModel GetSellerDetailForSeller(int id, int userId);
    SellerProductPageUserPanelQueryModel GetSellerProductsForUserPanel(int pageId,string filter,int sellerId, int userId);
    List<SellersUserPanelQueryModel> GetSellersForUser(int userId);
    List<SellersForAddDiscountUserPanelQueryModel> GetSellersForUserForAddDiscount(int userId);
    List<int> GetUserSellerIds(int userId);
    Task<bool> IsProductSellForUser(int userId, int id);
    bool IsSellerForUser(int id, int userId);
}

public class SellersForAddDiscountUserPanelQueryModel
{
    public int Id { get; set; }
    public string Title { get; set; }
}

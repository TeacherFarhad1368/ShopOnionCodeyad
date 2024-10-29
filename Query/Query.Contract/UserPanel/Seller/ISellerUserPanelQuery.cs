using Shared.Domain.Enum;

namespace Query.Contract.UserPanel.Seller;
public interface ISellerUserPanelQuery
{
    List<SellersUserPanelQueryModel> GetSellersForUser(int userId); 
}

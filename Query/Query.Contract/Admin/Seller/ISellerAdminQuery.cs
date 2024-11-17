namespace Query.Contract.Admin.Seller;
public interface ISellerAdminQuery
{
    List<SellerRequestAdminQueryModel> GetSellerRequestsForAdmin();
    SellerRequestDetailAdminQueryModel GetSellerRequestDetailForAdmin(int id);
    SellerDetailAdminQueryModel GetSellerDetailForAdmin(int id);
    SellerAdminPaging GetSellersForAdmin(int pageId, int take, string filter);
}

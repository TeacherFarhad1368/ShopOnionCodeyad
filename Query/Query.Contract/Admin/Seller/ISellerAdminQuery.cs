namespace Query.Contract.Admin.Seller;
public interface ISellerAdminQuery
{
    List<SellerRequestAdminQueryModel> GetSellerRequestsForAdmin();
    SellerRequestDetailAdminQueryModel GetSellerRequestDetailForAdmin(int id);
}

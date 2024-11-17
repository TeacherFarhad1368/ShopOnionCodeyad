using Shared;

namespace Query.Contract.Admin.Seller;

public class SellerAdminPaging : BasePaging
{
    public string Filter { get; set; }
    public List<SellerAdminQueryModel> Sellers { get; set; }
}

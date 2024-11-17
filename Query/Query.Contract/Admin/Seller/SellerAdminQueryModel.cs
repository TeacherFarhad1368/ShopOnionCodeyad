namespace Query.Contract.Admin.Seller;

public class SellerAdminQueryModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Title { get; set; }
    public int StateId { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; }
    public string ImageName { get; set; }
    public string Phone1 { get; set; }
    public string? Email { get; set; }
    public string CreateDate { get; set; }
    public string UpdateDate { get; set; }
}
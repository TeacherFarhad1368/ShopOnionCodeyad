using Shared.Domain.Enum;

namespace Query.Contract.Admin.Seller;

public class SellerRequestDetailAdminQueryModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Title { get; set; }
    public int StateId { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; }
    public string Address { get; set; }
    public string? GoogleMapUrl { get; set; }
    public string ImageAccept { get; set; }
    public string ImageName { get; set; }
    public string ImageAlt { get; set; }
    public SellerStatus Status { get; set; }
    public string? WhatsApp { get; set; }
    public string? Telegram { get; set; }
    public string? Instagram { get; set; }
    public string Phone1 { get; set; }
    public string? Phone2 { get; set; }
    public string? Email { get; set; }
    public string CreateDate { get; set; }
    public string UpdateDate { get; set; }
}
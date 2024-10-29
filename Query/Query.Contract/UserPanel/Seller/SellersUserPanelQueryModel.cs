using Shared.Domain.Enum;

namespace Query.Contract.UserPanel.Seller;

public class SellersUserPanelQueryModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int StateId { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; }
    public string ImageAccept { get; set; }
    public SellerStatus Status { get; set; }
    public string ImageName { get; set; }
    public string Phone1 { get; set; }
    public string CreationDate { get; set; }
}
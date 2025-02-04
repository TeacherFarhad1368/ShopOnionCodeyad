namespace Query.Contract.UserPanel.Order;

public class OrderAddressForOrderUserPanelQueryModel
{
    public int Id { get; set; }
    public int StateId { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; }
    public string StateName { get; set; }
    public string AddressDetail { get; set; }
    public string PostalCode { get; set; }
    public string Phone { get; set; }
    public string FullName { get; set; }
    public string IranCode { get; set; }
    public string CreationDate { get; set; }
}

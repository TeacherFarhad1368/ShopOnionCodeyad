using Shared.Domain;
using Shared.Domain.Enum;
using Shop.Domain.ProductSellAgg;
namespace Shop.Domain.SellerAgg;
public class Seller : BaseEntityCreateUpdateActive<int>
{
    public Seller(int userId, string title, int stateId, int cityId, string address, string? googleMapUrl, string imageAccept, string imageName, string imageAlt,
        string? whatsApp, string? telegram, string? instagram, string phone1, string? phone2, string? email)
    {
        UserId = userId;
        Title = title;
        StateId = stateId;
        CityId = cityId;
        Address = address;
        GoogleMapUrl = googleMapUrl;
        ImageAccept = imageAccept;
        ImageName = imageName;
        ImageAlt = imageAlt;
        WhatsApp = whatsApp;
        Telegram = telegram;
        Instagram = instagram;
        Phone1 = phone1;
        Phone2 = phone2;
        Email = email;
        Status = SellerStatus.درخواست_ارسال_شده;
    }
    public void ChangeStatus(SellerStatus status)
    {
        Status = status;
    }
    public void Edit( string title, int stateId, int cityId, string address, string? googleMapUrl, string imageName, string imageAlt,
        string? whatsApp, string? telegram, string? instagram, string phone1, string? phone2, string? email)
    {
        Title = title;
        StateId = stateId;
        CityId = cityId;
        Address = address;
        GoogleMapUrl = googleMapUrl;
        ImageName = imageName;
        ImageAlt = imageAlt;
        WhatsApp = whatsApp;
        Telegram = telegram;
        Instagram = instagram;
        Phone1 = phone1;
        Phone2 = phone2;
        Email = email;
    }
    public void EditImageAccept(string imageAccept)
    {
        ImageAccept = imageAccept;
    }
    public int UserId { get; private set; }
    public string Title { get; private set; }
    public int StateId { get; private set; }
    public int CityId { get; private set; }
    public string Address { get; private set; }
    public string? GoogleMapUrl { get; private set; }
    public string ImageAccept { get; private set; }
    public string ImageName { get; private set; }
    public string ImageAlt { get; private set; }
    public SellerStatus Status { get; private set; }
    public string? WhatsApp { get; private set; }
    public string? Telegram { get; private set; }
    public string? Instagram { get; private set; }
    public string Phone1 { get; private set; }
    public string? Phone2 { get; private set; }
    public string? Email { get; private set; }
    public List<ProductSell> ProductSells { get; private set; }
    public Seller()
    {
        ProductSells = new();
    }
}

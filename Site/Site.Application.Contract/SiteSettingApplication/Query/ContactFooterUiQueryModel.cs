namespace Site.Application.Contract.SiteSettingApplication.Query;

public class ContactFooterUiQueryModel
{
    public ContactFooterUiQueryModel(string address, string phone, string email, string android, string iOS)
    {
        Address = address;
        Phone = phone;
        Email = email;
        Android = android;
        IOS = iOS;
    }

    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Android { get; set; }
    public string IOS { get; set; }
}
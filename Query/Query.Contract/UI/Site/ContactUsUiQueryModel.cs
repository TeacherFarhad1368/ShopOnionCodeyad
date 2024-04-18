namespace Query.Contract.UI.Site;

public class ContactUsUiQueryModel
{
    public ContactUsUiQueryModel(string description, string phone1, string phone2, 
        string email1, string email2, string address, SeoUiQueryModel seo, List<BreadCrumbQueryModel> breadCrumbs)
    {
        Description = description;
        Phone1 = phone1;
        Phone2 = phone2;
        Email1 = email1;
        Email2 = email2;
        Address = address;
        Seo = seo;
        BreadCrumbs = breadCrumbs;
    }

    public string Description { get; private set; }
    public string Phone1 { get; private set; }
    public string Phone2 { get;private set; }
    public string Email1 { get;private set; }
    public string Email2 { get;private set; }
    public string Address { get; private set; }
    public SeoUiQueryModel Seo { get; private set; }
    public List<BreadCrumbQueryModel> BreadCrumbs { get; private set; }
}
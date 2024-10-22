namespace Shop.Application.Contract.SellerApplication.Command;

public class EditSellerRequest : RequestSeller
{
    public int Id { get; set; }
    public string ImageName { get; set; }
    public string ImageAcceptName { get; set; }
}

namespace Query.Contract.UI.Product;

public class WishListForUserPanelQueryModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string ImageAddress { get; set; }
    public string ImageAlt { get; set; }
}
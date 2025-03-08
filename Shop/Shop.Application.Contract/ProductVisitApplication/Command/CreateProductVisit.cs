namespace Shop.Application.Contract.ProductVisitApplication.Command;

public class CreateProductVisit
{
    public CreateProductVisit(int productId, int userId, int count)
    {
        ProductId = productId;
        UserId = userId;
        Count = count;
    }

    public int ProductId { get; private set; }
    public int UserId { get; private set; }
    public int Count { get; private set; }
}
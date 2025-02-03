namespace Shop.Application.Contract.OrderApplication.Command;
public interface IOrderApplication
{
    Task<bool> UbsertOpenOrderForUserAsync(int _userId,List<ShopCartViewModel> cart);
}

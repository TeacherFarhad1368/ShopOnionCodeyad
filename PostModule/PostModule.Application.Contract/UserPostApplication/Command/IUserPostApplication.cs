namespace PostModule.Application.Contract.UserPostApplication.Command;

public interface IUserPostApplication
{
    Task<bool> CreatePostOrderAsync(CreatePostOrder command);
    Task<bool> PaymentPostOrderAsync(PaymentPostModel command);
    Task<CreatePostOrder> GetCreatePostModelAsync(int userId, int packageId);
    Task<PostOrderUserPanelModel> GetPostOrderNotPaymentForUser(int userId);
}

using PostModule.Application.Contract.UserPostApplication.Command;
using Shared.Domain;

namespace PostModule.Domain.UserPostAgg
{
    public interface IPOstOrderRepository : IRepository<int, PostOrder>
    {
        Task<PostOrderUserPanelModel> GetPostOrderNotPaymentForUser(int userId);
        Task<PostOrder> GetPostOrderNotPaymentForUserAsync(int userId);
    }
}

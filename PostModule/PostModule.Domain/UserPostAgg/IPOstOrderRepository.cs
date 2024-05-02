using Shared.Domain;

namespace PostModule.Domain.UserPostAgg
{
    public interface IPOstOrderRepository : IRepository<int, PostOrder>
    {
        Task<PostOrder> GetPostOrderNotPaymentForUserAsync(int userId);
    }
}

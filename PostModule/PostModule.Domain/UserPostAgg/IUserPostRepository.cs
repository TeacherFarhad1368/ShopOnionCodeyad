using Shared.Domain;

namespace PostModule.Domain.UserPostAgg
{
    public interface IUserPostRepository : IRepository<int, UserPost>
    {
        Task<UserPost> GetForUser(int userId);
        Task<UserPost> GetByApiCode(string apiCode);
    }
}

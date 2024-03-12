using Shared.Infrastructure;
using Users.Domain.UserAgg;
using Users.Domain.UserAgg.Repository;

namespace Users.Infrastructure.Service
{
    internal class UserRepository : Repository<int,User> , IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context) : base(context)
        {
            _context = context;
        }

        public User GetByMobile(string v) =>
            _context.Users.SingleOrDefault(u => u.Mobile == v.Trim());
    }
}

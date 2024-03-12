using Shared.Infrastructure;
using Users.Domain.UserAgg;
using Users.Domain.UserAgg.Repository;

namespace Users.Infrastructure.Service
{
    internal class UserAddressRepository : Repository<int,UserAddress> , IUserAdressRepository
    {
        private readonly UserContext _context;
        public UserAddressRepository(UserContext context) : base(context)
        {
            _context = context;
        }
    }
}

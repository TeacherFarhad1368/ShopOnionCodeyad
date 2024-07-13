using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Contract.UserApplication.Command;

namespace Users.Domain.UserAgg.Repository
{
    public interface IUserRepository : IRepository<int, User>
    {
        User GetByMobile(string v);
        EditUserByAdmin GetForEditByAdmin(int userId);
        EditUserByUser GetForEditByUser(int userId);
    }
}

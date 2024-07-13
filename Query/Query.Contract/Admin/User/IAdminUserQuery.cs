using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.Admin.User;

public interface IAdminUserQuery
{
    UsersForAdminPaging GetUsersForAdmin(int pageId, string filter, int take,
        UserOrderBySearch orderBy, UserStatusSearch status);
}

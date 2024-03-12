using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Contract.RoleApplication.Query
{
    public interface IRoleQuery
    {
        bool CheckPermission(int userId, UserPermission permission);
        List<RoleQueryModel> GetAllRoles();
    }
}

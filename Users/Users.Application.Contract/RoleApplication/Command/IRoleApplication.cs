using Shared.Application;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Contract.RoleApplication.Command
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command,List<UserPermission> permissions);
        OperationResult Edit(EditRole command, List<UserPermission> permissions);
        OperationResult EditUserRole(int userId, List<int> roles);
        EditRole GetForEdit(int id);
        List<RolePermissionQueryModel> GetPermissionsForRole(int id);
    }
    public class RolePermissionQueryModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public UserPermission UserPermission { get; set; }
    }
}

using Shared.Application;
using Shared.Domain;
using Shared.Domain.Enum;
using Users.Application.Contract.RoleApplication.Command;
using Users.Application.Contract.RoleApplication.Query;

namespace Users.Domain.UserAgg.Repository
{
    public interface IRoleRepository : IRepository<int, Role>
    {
        bool CheckPermission(int userId, UserPermission permission);
        OperationResult CreateRole(CreateRole command, List<UserPermission> permissions);
        OperationResult EditRole(EditRole command, List<UserPermission> permissions);
        OperationResult EditUserRole(int userId, List<int> roles);
        List<RoleQueryModel> GetAllRoles();
        EditRole GetForEdit(int id);
        List<RolePermissionQueryModel> GetPermissionsForRole(int id);
    }
}

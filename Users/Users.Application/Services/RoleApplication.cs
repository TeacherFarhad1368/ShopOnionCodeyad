using Shared.Application;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Contract.RoleApplication.Command;
using Users.Domain.UserAgg.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Users.Application.Services
{
    internal class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;
        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;

        }

        public OperationResult Create(CreateRole command, List<UserPermission> permissions)
        {
           return _roleRepository.CreateRole(command, permissions);
        }

        public OperationResult Edit(EditRole command, List<UserPermission> permissions)
        {
            return _roleRepository.EditRole(command, permissions);
        }

        public OperationResult EditUserRole(int userId, List<int> roles)
        {
            return _roleRepository.EditUserRole(userId, roles);
        }

        public EditRole GetForEdit(int id) =>
            _roleRepository.GetForEdit(id);

        public List<RolePermissionQueryModel> GetPermissionsForRole(int id) =>
            _roleRepository.GetPermissionsForRole(id);
    }
}

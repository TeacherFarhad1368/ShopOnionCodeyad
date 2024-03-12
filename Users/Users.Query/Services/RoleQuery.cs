using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Contract.RoleApplication.Query;
using Users.Domain.UserAgg.Repository;
using Users.Infrastructure;

namespace Users.Query.Services
{
    internal class RoleQuery : IRoleQuery
    {
        private readonly IRoleRepository _roleRepository;
        public RoleQuery(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public bool CheckPermission(int userId, UserPermission permission)=>
            _roleRepository.CheckPermission(userId,permission);

        public List<RoleQueryModel> GetAllRoles() =>
            _roleRepository.GetAllRoles();
    }
}

using Microsoft.EntityFrameworkCore;
using Shared.Application;
using Shared.Domain.Enum;
using Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Contract.RoleApplication.Command;
using Users.Application.Contract.RoleApplication.Query;
using Users.Domain.UserAgg;
using Users.Domain.UserAgg.Repository;

namespace Users.Infrastructure.Service
{
    internal class RoleRepository : Repository<int, Role> , IRoleRepository
    {
        private readonly UserContext _context;
        public RoleRepository(UserContext context) : base(context)
        {
            _context = context;
        }

        public bool CheckPermission(int userId, UserPermission permission)
        {
            var userRoles = _context.UserRoles.Include(u=>u.Role)
                .ThenInclude(r=>r.Permissions).Where(u=>u.UserId == userId);
            if (userRoles.Count() < 1) return false;
            foreach(var item in userRoles.ToList())
            {
                if(item.Role.Permissions.Any(p=>p.UserPermission == permission))
                    return true;    
            }
            return false;
        }

        public OperationResult CreateRole(CreateRole command, List<UserPermission> permissions)
        {
            if (ExistBy(r => r.Title.Trim() == command.Title.Trim()))
                return new(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));
            if(permissions.Count() < 1)
                return new(false, ValidationMessages.ChoosePermission, nameof(command.Title));
            Role role = new(command.Title.Trim());
            if (Create(role))
            {
                if(permissions.Count() > 0)
                {
                    permissions.ForEach(x =>
                    {
                        Permission permission = new(role.Id, x);
                        _context.Permissions.Add(permission);
                    });
                    Save();
                }
                return new(true);
            }
            return new(false, ValidationMessages.SystemErrorMessage, "Title");
        }

        public OperationResult EditRole(EditRole command, List<UserPermission> permissions)
        {
            if (ExistBy(r => r.Title.Trim() == command.Title.Trim() && r.Id != command.Id))
                return new(false, ValidationMessages.DuplicatedMessage, "Title");
            if (permissions.Count() < 1)
                return new(false, ValidationMessages.ChoosePermission, nameof(command.Title));
            Role role = GetById(command.Id);
            role.Edit(command.Title.Trim());
            var oldPermissions = _context.Permissions.Where(p => p.RoleId == role.Id).ToList();
            _context.Permissions.RemoveRange(oldPermissions);
            permissions.ForEach(x =>
            {
                Permission permission = new(role.Id, x);
                _context.Permissions.Add(permission);
            });

            if (Save()) return new(true);
            return new(false, ValidationMessages.SystemErrorMessage, "Title");
        }

        public OperationResult EditUserRole(int userId, List<int> roles)
        {
            var oldUserRoles = _context.UserRoles.Where(u=>u.UserId == userId).ToList();
            if (oldUserRoles.Count() > 0)
                _context.UserRoles.RemoveRange(oldUserRoles);

            if (roles.Count() > 0)
                roles.ForEach(r =>
                {
                    UserRole userRole = new(userId, r);
                    _context.UserRoles.Add(userRole);
                });

            if (Save()) return new(true);
            return new(false, ValidationMessages.SystemErrorMessage, "Title");
        }

        public List<RoleQueryModel> GetAllRoles()
        {
            return _context.Roles.Include(r=>r.UserRoles)
                .ThenInclude(u=>u.User).Where(r => r.Id != 2)
                .Select(r => new RoleQueryModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    UserRoles = r.UserRoles.Select(u=> new UserRoleQueryModel
                    {
                        Id = u.Id,
                        UserAvatar = u.User.Avatar,
                        UserId = u.UserId,
                        UserName = u.User.FullName ?? u.User.Mobile
                    }).ToList()
                }).ToList();
        }

        public EditRole GetForEdit(int id) =>
            _context.Roles.Select(r => new EditRole()
            {
                Id = r.Id,
                Title = r.Title
            }).SingleOrDefault(r=>r.Id == id);

        public List<RolePermissionQueryModel> GetPermissionsForRole(int id) =>
            _context.Permissions.Where(p => p.RoleId == id)
            .Select(r => new RolePermissionQueryModel
            {
                Id= r.Id,
                UserPermission = r.UserPermission,
                RoleId = r.RoleId
            }).ToList();

        public bool IsUserAdmin(int userId)
        {
            return _context.UserRoles.Any(r => r.UserId == userId); 
        }
    }
}

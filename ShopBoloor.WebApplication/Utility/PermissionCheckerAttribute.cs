using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enum;
using Users.Application.Contract.UserApplication.Command;
using Users.Application.Contract.RoleApplication.Query;
using System.Security.Claims;

namespace ShopBoloor.WebApplication.Utility
{
    internal class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IRoleQuery _roleQuery;
        private readonly UserPermission _permissionId;
        public PermissionCheckerAttribute(UserPermission permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _roleQuery = (IRoleQuery)context.HttpContext.RequestServices.
               GetService(typeof(IRoleQuery));
            if (context.HttpContext.User.Claims.Count() > 0)
            {
                string userId = context.HttpContext.User.Claims.
                FirstOrDefault(x => x.Type == "UserId").Value;
                if (_roleQuery.CheckPermission(int.Parse(userId), _permissionId ) == false)
                    context.Result = new RedirectResult("/Auth/AccessDenied");
            }
            else
                context.Result = new RedirectResult("/Auth/Login");
        }
    }
}
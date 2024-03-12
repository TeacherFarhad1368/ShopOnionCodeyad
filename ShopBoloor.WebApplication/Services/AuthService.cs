using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Shared.Application.Services.Auth;
using System.Security.Claims;

namespace ShopBoloor.WebApplication.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetLoginUserAvatar()
        {
            if (IsUserLogin())
                return _contextAccessor.HttpContext.User.Claims.
                    Single(x => x.Type == "Avatar").Value;

            return "";
        }

        public string GetLoginUserFullName()
        {
            if (IsUserLogin())
                return _contextAccessor.HttpContext.User.Claims.
                    Single(x => x.Type == "FullName").Value;

            return "";
        }

        public int GetLoginUserId()
        {
            if (IsUserLogin())
                return int.Parse(_contextAccessor.HttpContext.User.Claims.
                    Single(x => x.Type == "UserId").Value);

            return 0;
        }

        public bool IsUserLogin()=>
            _contextAccessor.HttpContext.User.Claims.Count() > 0;

        public void Login(AuthModel command)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , command.Mobile),
                new("Avatar",command.Avatar),
                new("UserId",command.UserId.ToString()),
                new("FullName",command.FullName)
            };

            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties authenticationProperties = new()
            {
                IsPersistent = true
            };
            _contextAccessor.HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authenticationProperties);
        }

        public void Logout()
        {
            _contextAccessor.HttpContext.SignOutAsync();
        }
    }
}

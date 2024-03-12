using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Services.Auth
{
    public interface IAuthService
    {
        void Login(AuthModel command);
        void Logout();
        int GetLoginUserId();
        string GetLoginUserAvatar();
        string GetLoginUserFullName();
        bool IsUserLogin();
    }
}

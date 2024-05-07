using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Contract.UserApplication.Command
{
    public interface IUserApplication
    {
        bool Register(RegisterUser command);
        OperationResult Login(LoginUser command);
        OperationResult Create(CreateUser command);
        OperationResult Edit(EditUserByAdmin command);
        OperationResult EditByUser(EditUserByUser command,int userId);
        OperationResult ChangePassword(ChangeUserPassword command);
        EditUserByUser GetForEditByUser(int userId);
        bool ActivationChange(int id);
        bool DeleteChange(int id);
        void Logout();
    }
}

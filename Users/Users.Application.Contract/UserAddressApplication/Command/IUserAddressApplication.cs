using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Contract.UserAddressApplication.Command
{
    public interface IUserAddressApplication
    {
        OperationResult Create(CreateAddress command, int userId);
        bool Delete(int id,int userId);
    }
}

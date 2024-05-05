using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.UserPanel.User;

public interface IUserPanelQuery
{
    UserInfoForPanelQueryModel GetUserInfoForPanel(int userId);
}

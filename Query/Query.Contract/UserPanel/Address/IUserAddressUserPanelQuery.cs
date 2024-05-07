using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.UserPanel.Address;

public interface IUserAddressUserPanelQuery
{
    List<UserAddressForPanelQueryModel> GetAddresseForUserPanel(int userId);
}

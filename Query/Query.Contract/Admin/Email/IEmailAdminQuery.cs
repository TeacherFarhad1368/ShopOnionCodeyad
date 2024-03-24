using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.Admin.Email;

public interface IEmailAdminQuery
{
	EmailUserAdminPaging GetEmailUsersForAdmin(int pageId, int take, string filter);
}

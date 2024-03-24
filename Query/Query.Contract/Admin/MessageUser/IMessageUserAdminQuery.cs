using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.Admin.MessageUser;

public interface IMessageUserAdminQuery
{
	MessageUserAdminPaging GetMessagesForAdmin(int pageId, int take, string filter, MessageStatus status);
	MessageUserDetailAdminQueryModel GetMessageDetailForAdmin(int id);
}

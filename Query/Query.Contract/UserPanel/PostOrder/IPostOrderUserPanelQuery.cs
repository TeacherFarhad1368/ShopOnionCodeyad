using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.UserPanel.PostOrder;

public interface IPostOrderUserPanelQuery
{
    PostOrderUserPanelPaging GetPostOrdersForUsePanel(int pageId, int userId);
}

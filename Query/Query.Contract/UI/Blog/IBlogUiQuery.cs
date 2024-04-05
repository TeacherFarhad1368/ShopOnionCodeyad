using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.UI.Blog;

public interface IBlogUiQuery
{
    BlogUiPaging GetBlogsForUi(string slug, int pageId, string filter);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.MenuApplication.Query
{
    public interface IMenuQuery
    {
        List<MenuForAdminQueryModel> GetForAdmin(int? parentId);
        List<MenuForUi> GetForIndex();
        List<MenuForUi> GetForBlog();

    }
}

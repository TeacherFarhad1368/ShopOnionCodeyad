using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.MenuApplication.Query
{
    public interface IMenuQuery
    {
        MenuPageAdminQueryModel GetForAdmin(int parentId);
        List<MenuForUi> GetForIndex();
        List<MenuForUi> GetForFooter();
        List<MenuForUi> GetForBlog();

    }
}

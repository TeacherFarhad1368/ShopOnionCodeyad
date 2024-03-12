using Site.Application.Contract.MenuApplication.Query;

namespace Site.Query.Services;

internal class MenuQuery : IMenuQuery
{
    public List<MenuForAdminQueryModel> GetForAdmin(int? parentId)
    {
        throw new NotImplementedException();
    }

    public List<MenuForUi> GetForBlog()
    {
        throw new NotImplementedException();
    }

    public List<MenuForUi> GetForIndex()
    {
        throw new NotImplementedException();
    }
}

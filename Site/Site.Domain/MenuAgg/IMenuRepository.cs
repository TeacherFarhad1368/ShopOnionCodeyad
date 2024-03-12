using Shared.Domain;
using Site.Application.Contract.MenuApplication.Command;

namespace Site.Domain.MenuAgg
{
    public interface IMenuRepository : IRepository<int, Menu>
    {
        EditMenu GetForEdit(int id);
    }
}

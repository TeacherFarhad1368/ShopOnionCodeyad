using Shared.Domain.Enum;

namespace Site.Application.Contract.MenuApplication.Query
{
    public class MenuPageAdminQueryModel
    {
        public int Id { get; set; }
        public string PageTitle { get; set; }
        public MenuStatus? Status { get; set; }
        public List<MenuForAdminQueryModel> Menus { get; set; }
    }
}

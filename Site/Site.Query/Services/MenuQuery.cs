using Shared.Application;
using Shared.Domain.Enum;
using Site.Application.Contract.MenuApplication.Query;
using Site.Domain.MenuAgg;

namespace Site.Query.Services;

internal class MenuQuery : IMenuQuery
{
    private readonly IMenuRepository _menuRepository;

    public MenuQuery(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public MenuPageAdminQueryModel GetForAdmin(int parentId)
    {
        MenuPageAdminQueryModel model = new()
        {
            Id = parentId
        };
        if (parentId == 0)
        {
            model.PageTitle = "لیست منو های سردسته";
            model.Menus = _menuRepository.GetAllByQuery(m => m.ParentId == null)
                .Select(m => new MenuForAdminQueryModel
                {
                    Active = m.Active,
                    CreationDate = m.CreateDate.ToPersainDate(),
                    Id = m.Id,
                    Number = m.Number,
                    Status = m.Status,
                    Title = m.Title,
                    Url = m.Url,
                    ImageName = m.ImageName
                }).ToList();
        }
        else
        {
            var menuParent = _menuRepository.GetById(parentId);
            model.PageTitle = $"لیست زیر منو های {menuParent.Title} - وضعیت {menuParent.Status.ToString().Replace("_"," ")}";
            model.Status = menuParent.Status;
            model.Menus = _menuRepository.GetAllByQuery(m => m.ParentId == parentId)
               .Select(m => new MenuForAdminQueryModel
               {
                   Active = m.Active,
                   CreationDate = m.CreateDate.ToPersainDate(),
                   Id = m.Id,
                   Number = m.Number,
                   Status = m.Status,
                   Title = m.Title,
                   Url = m.Url,
                   ImageName =  m.ImageName
               }).ToList();
        }
        return model;
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

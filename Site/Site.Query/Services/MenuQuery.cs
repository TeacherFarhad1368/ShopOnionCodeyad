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
        List<MenuForUi> model = new();
        var menus = _menuRepository.GetAllByQuery(b => b.Active &&
        (b.Status == MenuStatus.منوی_وبلاگ_لینک
        || b.Status == MenuStatus.منوی_وبلاگ_با_زیرمنوی_بدون_عکس
        || b.Status == MenuStatus.منوی_وبلاگ_با_زیر_منوی_عکس_دار));
        foreach(var item in menus)
        {
            MenuForUi menu = new()
            {
                Number = item.Number,
                Title = item.Title,
                Url = item.Url,
                Status = item.Status,
                Childs = new()
            };
            if(_menuRepository.ExistBy(m=>m.ParentId == item.Id && m.Active))
                menu.Childs = _menuRepository.GetAllByQuery(m => m.Active && m.ParentId == item.Id)
                .Select(m => new MenuForUi
                {
                    ImageAlt = m.ImageAlt,
                    Childs = new(),
                    ImageName = FileDirectories.MenuImageDirectory + m.ImageName,
                    Number = m.Number,
                    Title = m.Title,
                    Url = m.Url,
                    Status = m.Status
                }).ToList();

            model.Add(menu);
        }
        return model;
    }

    public List<MenuForUi> GetForFooter()
    {
        List<MenuForUi> model = new();
        var menus = _menuRepository.GetAllByQuery(b => b.Active &&
        (b.Status == MenuStatus.تیتر_منوی_فوتر));
        foreach (var item in menus)
        {
            MenuForUi menu = new()
            {
                Number = item.Number,
                Title = item.Title,
                Url = item.Url,
                Status = item.Status,
                Childs = new()
            };
            if (_menuRepository.ExistBy(m => m.ParentId == item.Id && m.Active))
                menu.Childs = _menuRepository.GetAllByQuery(m => m.Active && m.ParentId == item.Id)
                .Select(m => new MenuForUi
                {
                    Number = m.Number,
                    Title = m.Title,
                    Url = m.Url,
                    Status = m.Status
                }).ToList();

            model.Add(menu);
        }
        return model;
    }

    public List<MenuForUi> GetForIndex()
    {
        List<MenuForUi> model = new();
        var menus = _menuRepository.GetAllByQuery(b => b.Active &&
        (b.Status == MenuStatus.منوی_اصلی
        || b.Status == MenuStatus.منوی_اصلی_با_زیر_منو
        ));
        foreach (var item in menus)
        {
            MenuForUi menu = new()
            {
                Number = item.Number,
                Title = item.Title,
                Url = item.Url,
                ImageAlt = item.ImageAlt,
                ImageName = string.IsNullOrEmpty(item.ImageName) ? "" : FileDirectories.MenuImageDirectory + item.ImageName,
                Childs = new(),
                Status = item.Status
            };
            if (_menuRepository.ExistBy(m => m.ParentId == item.Id && m.Active))
                menu.Childs = _menuRepository.GetAllByQuery(m => m.Active && m.ParentId == item.Id)
                .Select(m => new MenuForUi
                {
                    Id= m.Id,
                    Childs = new(),
                    Number = m.Number,
                    Title = m.Title,
                    Url = m.Url,
                    Status = m.Status
                }).ToList();

            model.Add(menu);
        }
        foreach(var item in model.Where(w=>w.Status == MenuStatus.منوی_اصلی_با_زیر_منو && w.Childs.Count() > 0))
        {
            foreach(var sub in item.Childs)
            {
                sub.Childs = _menuRepository.GetAllByQuery(m => m.Active && m.ParentId == sub.Id)
                .Select(m => new MenuForUi
                {
                    Childs = new(),
                    Number = m.Number,
                    Title = m.Title,
                    Url = m.Url,
                    Status = m.Status
                }).ToList();
            }
        }
        return model;
    }
}

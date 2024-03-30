using Shared.Application;
using Shared.Application.Services;
using Site.Application.Contract.MenuApplication.Command;
using Site.Domain.MenuAgg;
using Shared;
using Site.Domain.BanerAgg;
using Shared.Domain.Enum;

namespace Site.Application.Services
{
    internal class MenuApplication : IMenuApplication
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IFileService _fileService;

        public MenuApplication(IMenuRepository menuRepository, IFileService fileService)
        {
            _menuRepository = menuRepository;
            _fileService = fileService;
        }

        public bool ActivationChange(int id)
        {
            var menu = _menuRepository.GetById(id);
            menu.ActivationChange();
            return _menuRepository.Save();
        }

        public OperationResult Create(CreateMenu command)
        {
            if(command.Status == Shared.Domain.Enum.MenuStatus.منوی_اصلی_با_زیر_منو)
            {
                if(command.ImageFile == null || !command.ImageFile.IsImage())
                    return new(false, $"{MenuStatus.منوی_اصلی_با_زیر_منو.ToString().Replace("_"," ")} نیاز به یک تصویر دارد", nameof(command.ImageFile));
                else if(string.IsNullOrEmpty(command.ImageAlt))
                    return new(false, ValidationMessages.RequiredMessage, nameof(command.ImageAlt));
            }
            else
            {
                if(command.ImageFile != null)
                    return new(false, $"{MenuStatus.منوی_اصلی_با_زیر_منو.ToString().Replace("_", " ")} نیاز به تصویر ندارد", nameof(command.ImageFile));
                if(!string.IsNullOrEmpty(command.ImageAlt))
                    return new(false, $"{MenuStatus.منوی_اصلی_با_زیر_منو.ToString().Replace("_", " ")} نیاز به Alt تصویر ندارد", nameof(command.ImageAlt));
            }
            string imageName = "";
            if(command.ImageFile != null)
            {
                 imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.MenuImageFolder);
                if (imageName == "")
                    return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));

                _fileService.ResizeImage(imageName, FileDirectories.MenuImageFolder, 100);
            }
            
            Menu menu = new(command.Number, command.Title, command.Url, command.Status, imageName, command.ImageAlt, null);
            if (_menuRepository.Create(menu))
                return new(true);
            if(command.ImageFile != null)
            {
                _fileService.DeleteImage($"{FileDirectories.MenuImageDirectory}{imageName}");
                _fileService.DeleteImage($"{FileDirectories.MenuImageDirectory100}{imageName}");
            }
            return new(false, ValidationMessages.SystemErrorMessage, nameof(command.ImageAlt));
        }

        public OperationResult CreateSub(CreateSubMenu command)
        {
            if(command.ParentStatus == MenuStatus.منوی_وبلاگ_با_زیر_منوی_عکس_دار)
            {
                if (command.ImageFile == null || !command.ImageFile.IsImage())
                    return new(false, $"{MenuStatus.منوی_وبلاگ_با_زیر_منوی_عکس_دار.ToString().Replace("_", " ")} نیاز به یک تصویر دارد", nameof(command.ImageFile));
                else if (string.IsNullOrEmpty(command.ImageAlt))
                    return new(false, ValidationMessages.RequiredMessage, nameof(command.ImageAlt));
            }
            else
            {
                if (command.ImageFile != null)
                    return new(false, " نیاز به تصویر ندارد", nameof(command.ImageFile));
                if (!string.IsNullOrEmpty(command.ImageAlt))
                    return new(false, " نیاز به Alt تصویر ندارد", nameof(command.ImageAlt));
            }
            string imageName = "";
            if (command.ImageFile != null)
            {
                imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.MenuImageFolder);
                if (imageName == "")
                    return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));

                _fileService.ResizeImage(imageName, FileDirectories.MenuImageFolder, 100);
            }
            MenuStatus status = MenuStatus.منوی_اصلی;
            switch (command.ParentStatus)
            {
                case MenuStatus.منوی_اصلی_با_زیر_منو:
                    status = MenuStatus.زیرمنوی_سردسته;
                    break;
                case MenuStatus.زیرمنوی_سردسته:
                    status = MenuStatus.زیرمنو;
                    break;
                case MenuStatus.تیتر_منوی_فوتر:
                    status = MenuStatus.منوی_فوتر;
                    break;
                case MenuStatus.منوی_وبلاگ_با_زیرمنوی_بدون_عکس:
                    status = MenuStatus.زیر_منوی_وبلاگ;
                    break;
                case MenuStatus.منوی_وبلاگ_با_زیر_منوی_عکس_دار:
                    status = MenuStatus.زیر_منوی_وبلاگ;
                    break;
                default:
                    return new(false,ValidationMessages.SystemErrorMessage, nameof(command.Title));
            }
            Menu menu = new(command.Number, command.Title, command.Url, status, imageName, command.ImageAlt, command.ParentId);
            if (_menuRepository.Create(menu))
                return new(true);
            if (command.ImageFile != null)
            {
                _fileService.DeleteImage($"{FileDirectories.MenuImageDirectory}{imageName}");
                _fileService.DeleteImage($"{FileDirectories.MenuImageDirectory100}{imageName}");
            }
            return new(false, ValidationMessages.SystemErrorMessage, nameof(command.ImageAlt));
        }

        public OperationResult Edit(EditMenu command)
        {
            var menu = _menuRepository.GetById(command.Id); 
            string imageName = command.ImageName;
            string oldImageName = command.ImageName;
            if (command.ImageFile != null && string.IsNullOrEmpty(command.ImageAlt))
                return new(false, ValidationMessages.RequiredMessage, nameof(command.ImageAlt));
            if (command.ImageFile != null && !command.ImageFile.IsImage())
                return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
            if (command.ImageFile != null)
            {
                imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.MenuImageFolder);
                if (imageName == "")
                    return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));

                _fileService.ResizeImage(imageName, FileDirectories.MenuImageFolder, 100);
            }
            menu.Edit(command.Number, command.Title, command.Url, imageName, command.ImageAlt);
            if (_menuRepository.Save())
            {
                if(command.ImageFile != null && !string.IsNullOrEmpty(oldImageName))
                {
                    _fileService.DeleteImage($"{FileDirectories.MenuImageDirectory}{oldImageName}");
                    _fileService.DeleteImage($"{FileDirectories.MenuImageDirectory100}{oldImageName}");
                }
                return new(true);
            }
            if (command.ImageFile != null)
            {
                _fileService.DeleteImage($"{FileDirectories.MenuImageDirectory}{imageName}");
                _fileService.DeleteImage($"{FileDirectories.MenuImageDirectory100}{imageName}");
            }
            return new(false, ValidationMessages.SystemErrorMessage, nameof(command.ImageAlt));
        }

        public CreateSubMenu GetForCreate(int parentId)
        {
            var parent = _menuRepository.GetById(parentId);
            return new()
            {
                ImageAlt = "",
                ImageFile = null,
                ParentId = parent.Id,
                ParentStatus = parent.Status,
                ParentTitle = $"افزودن زیر منو برای {parent.Title}"
                
            };
        }

        public EditMenu GetForEdit(int id) =>
            _menuRepository.GetForEdit(id);
    }
}

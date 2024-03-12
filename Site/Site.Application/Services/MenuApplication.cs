using Shared.Application;
using Shared.Application.Services;
using Site.Application.Contract.MenuApplication.Command;
using Site.Domain.MenuAgg;
using Shared;
using Site.Domain.BanerAgg;

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

            if (command.ImageFile != null && string.IsNullOrEmpty(command.ImageAlt))
                return new(false, ValidationMessages.RequiredMessage, nameof(command.ImageAlt));
            if (command.ImageFile != null && !command.ImageFile.IsImage())
                return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
            string imageName = "";
            if(command.ImageFile != null)
            {
                 imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.MenuImageFolder);
                if (imageName == "")
                    return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));

                _fileService.ResizeImage(imageName, FileDirectories.MenuImageDirectory100, 100);
            }
            Menu menu = new(command.Number, command.Title, command.Url, command.Status, imageName, command.ImageAlt, command.ParentId);
            if (_menuRepository.Create(menu))
                return new(true);
            if(command.ImageFile != null)
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

                _fileService.ResizeImage(imageName, FileDirectories.MenuImageDirectory100, 100);
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

        public EditMenu GetForEdit(int id) =>
            _menuRepository.GetForEdit(id);
    }
}

using PostModule.Application.Contract.UserPostApplication.Command;
using PostModule.Domain.UserPostAgg;
using Shared;
using Shared.Application;
using Shared.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Services
{
    internal class PackageApplication : IPackageApplication
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IFileService _fileService;
        public PackageApplication(IPackageRepository packageRepository,IFileService fileService)
        {
            _packageRepository = packageRepository;
            _fileService = fileService;
        }

        public bool ActivationChange(int id)
        {
            var package = _packageRepository.GetById(id);
            package.ActivationChange();
            return _packageRepository.Save();
        }

        public OperationResult Create(CreatePackage command)
        {
            if (_packageRepository.ExistBy(p => p.Title.Trim() == command.Title.Trim()))
                return new(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));

            if (command.ImageFile == null || !command.ImageFile.IsImage())
                return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));

            string imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.PackageImageFolder);
            if (imageName == "")
                return new(false, ValidationMessages.SystemErrorMessage, "Title");

            _fileService.ResizeImage(imageName, FileDirectories.PackageImageFolder, 400);
            _fileService.ResizeImage(imageName, FileDirectories.PackageImageFolder, 100);


            Package package = new(command.Title, command.Description, command.Count, command.Price,imageName,command.ImageAlt);
            if (_packageRepository.Create(package))
            {
                return new(true);
            }
            _fileService.DeleteImage($"{FileDirectories.PackageImageDirectory}{imageName}");
            _fileService.DeleteImage($"{FileDirectories.PackageImageDirectory400}{imageName}");
            _fileService.DeleteImage($"{FileDirectories.PackageImageDirectory100}{imageName}");
            return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
        }

        public OperationResult Edit(EditPackage command)
        {
            var package = _packageRepository.GetById(command.Id);
            if (_packageRepository.ExistBy(p => p.Title.Trim() == command.Title.Trim() && p.Id != package.Id))
                return new(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));
            string imageName = command.ImageName;
            string oldImageName = command.ImageName;
            if (command.ImageFile != null)
            {
                imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.PackageImageFolder);
                if (imageName == "")
                    return new(false, ValidationMessages.SystemErrorMessage, "Title");
                _fileService.ResizeImage(imageName, FileDirectories.PackageImageFolder, 400);
                _fileService.ResizeImage(imageName, FileDirectories.PackageImageFolder, 100);
            }
            package.Edit(command.Title, command.Description, command.Count, command.Price,imageName,command.ImageAlt);
            if (_packageRepository.Save())
            {
                if (command.ImageFile != null)
                {
                    _fileService.DeleteImage($"{FileDirectories.PackageImageDirectory}{oldImageName}");
                    _fileService.DeleteImage($"{FileDirectories.PackageImageDirectory400}{oldImageName}");
                    _fileService.DeleteImage($"{FileDirectories.PackageImageDirectory100}{oldImageName}");
                }
                return new(true);
            }
            if (command.ImageFile != null)
            {
                _fileService.DeleteImage($"{FileDirectories.PackageImageDirectory}{imageName}");
                _fileService.DeleteImage($"{FileDirectories.PackageImageDirectory400}{imageName}");
                _fileService.DeleteImage($"{FileDirectories.PackageImageDirectory100}{imageName}");
            }
            return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
        }

        public EditPackage GetForEdit(int id) =>
            _packageRepository.GetForEdit(id);
    }
}

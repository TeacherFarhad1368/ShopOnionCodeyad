using Shared;
using Shared.Application;
using Shared.Application.Services;
using Site.Application.Contract.BanerApplication.Command;
using Site.Domain.BanerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Services
{
    internal class BanerApplication : IBanerApplication
    {
        private readonly IBanerRepository _banerRepository;
        private readonly IFileService _fileService;

        public BanerApplication(IBanerRepository banerRepository, IFileService fileService)
        {
            _banerRepository = banerRepository;
            _fileService = fileService;
        }

        public bool ActivationChange(int id)
        {
            var baner = _banerRepository.GetById(id);
            baner.ActivationChange();
            return _banerRepository.Save();
        }

        public OperationResult Create(CreateBaner command)
        {
            if (command.ImageFile == null || !command.ImageFile.IsImage())
                return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));

            string imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.BanerImageFolder);
            if (imageName == "")
                return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));

            _fileService.ResizeImage(imageName, FileDirectories.BanerImageFolder, 100);
            Baner baner = new(imageName, command.ImageAlt, command.Url, command.State);
            if (_banerRepository.Create(baner))
                return new(true);
            _fileService.DeleteImage($"{FileDirectories.BanerImageDirectory}{imageName}");
            _fileService.DeleteImage($"{FileDirectories.BanerImageDirectory100}{imageName}");
            return new(false, ValidationMessages.SystemErrorMessage, nameof(command.ImageAlt));
        }

        public OperationResult Edit(EditBaner command)
        {
            var baner = _banerRepository.GetById(command.Id);
            string imageName = baner.ImageName;
            string oldImageName = baner.ImageName;
            if (command.ImageFile != null)
            {
                if (!command.ImageFile.IsImage()) return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
                imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.BanerImageFolder);
                if (imageName == "")
                    return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
                _fileService.ResizeImage(imageName, FileDirectories.BanerImageFolder, 100);
            }
                baner.Edit(imageName, command.ImageAlt, command.Url);
                if (_banerRepository.Save())
                {
                    if(command.ImageFile != null)
                    {
                        _fileService.DeleteImage($"{FileDirectories.BanerImageDirectory}{oldImageName}");
                        _fileService.DeleteImage($"{FileDirectories.BanerImageDirectory100}{oldImageName}");
                    }
                    return new(true);
                }
                else
                {

                    if (command.ImageFile != null)
                    {
                        _fileService.DeleteImage($"{FileDirectories.BanerImageDirectory}{imageName}");
                        _fileService.DeleteImage($"{FileDirectories.BanerImageDirectory100}{imageName}");
                    }
                    return new(false, ValidationMessages.SystemErrorMessage, nameof(command.ImageAlt));
                }
            
        }

        public EditBaner GetForEdit(int id) =>
            _banerRepository.GetForEdit(id);
    }
}

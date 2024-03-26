using Shared.Application;
using Shared.Application.Services;
using Shared;
using Site.Application.Contract.SliderApplication.Command;
using Site.Domain.SliderAgg;

namespace Site.Application.Services
{
    internal class SliderApplication : ISliderApplication
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IFileService _fileService;

        public SliderApplication(ISliderRepository sliderRepository, IFileService fileService)
        {
            _sliderRepository = sliderRepository;
            _fileService = fileService;
        }

        public bool ActivationChange(int id)
        {
            var slider = _sliderRepository.GetById(id);
            slider.ActivationChange();
            return _sliderRepository.Save();
        }

        public OperationResult Create(CreateSlider command)
        {
            if (command.ImageFile == null || !command.ImageFile.IsImage())
                return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));

            string imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.SliderImageFolder);
            if (imageName == "")
                return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));

            _fileService.ResizeImage(imageName, FileDirectories.SliderImageFolder, 100);
            Slider slider = new(imageName, command.ImageAlt,command.Url);
            if (_sliderRepository.Create(slider)) return new(true);
            _fileService.DeleteImage($"{FileDirectories.SliderImageDirectory}{imageName}");
            _fileService.DeleteImage($"{FileDirectories.SliderImageDirectory100}{imageName}");
            return new(false, ValidationMessages.SystemErrorMessage, nameof(command.ImageAlt));
        }

        public OperationResult Edit(EditSlider command)
        {
            var slider = _sliderRepository.GetById(command.Id);
            string imageName = slider.ImageName;
            string oldImageName = slider.ImageName;
            if (command.ImageFile != null)
            {
                if (!command.ImageFile.IsImage()) return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
                imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.SliderImageFolder);
                if (imageName == "")
                    return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
                _fileService.ResizeImage(imageName, FileDirectories.SliderImageFolder, 100);
            }
            slider.Edit(imageName, command.ImageAlt,command.Url);
            if (_sliderRepository.Save())
            {
                if (command.ImageFile != null)
                {
                    _fileService.DeleteImage($"{FileDirectories.SliderImageDirectory}{oldImageName}");
                    _fileService.DeleteImage($"{FileDirectories.SliderImageDirectory100}{oldImageName}");
                }
                return new(true);
            }
            else
            {

                if (command.ImageFile != null)
                {
                    _fileService.DeleteImage($"{FileDirectories.SliderImageDirectory}{imageName}");
                    _fileService.DeleteImage($"{FileDirectories.SliderImageDirectory100}{imageName}");
                }
                return new(false, ValidationMessages.SystemErrorMessage, nameof(command.ImageAlt));
            }
        }

        public EditSlider GetForEdit(int id) =>
            _sliderRepository.GetForEdit(id);
    }
}

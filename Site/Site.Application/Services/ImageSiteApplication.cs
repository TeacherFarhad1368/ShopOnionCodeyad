using Shared;
using Shared.Application;
using Shared.Application.Services;
using Site.Application.Contract.ImageSiteApplication.Command;
using Site.Domain.SiteImageAgg;

namespace Site.Application.Services;

internal class ImageSiteApplication : IImageSiteApplication
{
	private readonly IImageSiteRepository _imageSiteRepository;
	private readonly IFileService _fileService;

	public ImageSiteApplication(IImageSiteRepository imageSiteRepository, IFileService fileService)
	{
		_imageSiteRepository = imageSiteRepository;
		_fileService = fileService;
	}

	public OperationResult Create(CreateImageSite command)
	{
		if(command.ImageFile == null || !command.ImageFile.IsImage())
            return new(false, ValidationMessages.ImageErrorMessage, nameof(command.Title));
        string imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.ImageFolder);
		if (imageName == "")
			return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));

		_fileService.ResizeImage(imageName, FileDirectories.ImageFolder, 100);

		SiteImage image = new(imageName, command.Title);
		if (_imageSiteRepository.Create(image)) return new(true);
		_fileService.DeleteImage($"{FileDirectories.ImageDirectory}{imageName}");
		_fileService.DeleteImage($"{FileDirectories.ImageDirectory100}{imageName}");
		return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
	}

	public bool DeleteFromDataBase(int id)
	{
		var image = _imageSiteRepository.GetById(id);
		string imageName = image.ImageName;
		if (_imageSiteRepository.Delete(image))
		{
			_fileService.DeleteImage($"{FileDirectories.ImageDirectory}{imageName}");
			_fileService.DeleteImage($"{FileDirectories.ImageDirectory100}{imageName}");
			return true;
		}
		return false;
	}
}
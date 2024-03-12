using Shared.Application;
using Shared.Application.Services;
using Site.Application.Contract.SiteServiceApplication.Command;
using Site.Domain.SiteServiceAgg;
using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Site.Domain.BanerAgg;
using System.Windows.Input;

namespace Site.Application.Services;

internal class SiteServiceApplication : ISiteServiceApplication
{
    private readonly ISiteServiceRepository _siteServiceepository;
    private readonly IFileService _fileService;

    public SiteServiceApplication(ISiteServiceRepository siteServiceepository, IFileService fileService)
    {
        _siteServiceepository = siteServiceepository;
        _fileService = fileService;
    }

    public bool ActivationChange(int id)
    {
        var service = _siteServiceepository.GetById(id);
        service.ActivationChange();
        return _siteServiceepository.Save();
    }

    public OperationResult Create(CreateSiteService commmand)
    {
        if (commmand.ImageFile == null || !commmand.ImageFile.IsImage())
            return new(false, ValidationMessages.ImageErrorMessage, nameof(commmand.ImageFile));

        string imageName = _fileService.UploadImage(commmand.ImageFile, FileDirectories.ServiceImageFolder);
        if (imageName == "")
            return new(false, ValidationMessages.ImageErrorMessage, nameof(commmand.ImageFile));

        _fileService.ResizeImage(imageName, FileDirectories.ServiceImageDirectory100, 100);
        SiteService service = new(imageName, commmand.ImageAlt, commmand.Title);
        if (_siteServiceepository.Create(service))
            return new(true);
        _fileService.DeleteImage($"{FileDirectories.ServiceImageDirectory}{imageName}");
        _fileService.DeleteImage($"{FileDirectories.ServiceImageDirectory100}{imageName}");
        return new(false, ValidationMessages.SystemErrorMessage, nameof(commmand.ImageAlt));
    }

    public OperationResult Edit(EditSiteService commmand)
    {
        var service = _siteServiceepository.GetById(commmand.Id);
        string imageName = service.ImageName;
        string oldImageName = service.ImageName;
        if (commmand.ImageFile != null)
        {
            if (!commmand.ImageFile.IsImage()) return new(false, ValidationMessages.ImageErrorMessage, nameof(commmand.ImageFile));
            imageName = _fileService.UploadImage(commmand.ImageFile, FileDirectories.ServiceImageFolder);
            if (imageName == "")
                return new(false, ValidationMessages.ImageErrorMessage, nameof(commmand.ImageFile));
            _fileService.ResizeImage(imageName, FileDirectories.ServiceImageDirectory100, 100);
        }
        service.Edit(imageName, commmand.ImageAlt, commmand.Title);
        if (_siteServiceepository.Save())
        {
            if (commmand.ImageFile != null)
            {
                _fileService.DeleteImage($"{FileDirectories.ServiceImageDirectory}{oldImageName}");
                _fileService.DeleteImage($"{FileDirectories.ServiceImageDirectory100}{oldImageName}");
            }
            return new(true);
        }
        else
        {

            if (commmand.ImageFile != null)
            {
                _fileService.DeleteImage($"{FileDirectories.ServiceImageDirectory}{imageName}");
                _fileService.DeleteImage($"{FileDirectories.ServiceImageDirectory100}{imageName}");
            }
            return new(false, ValidationMessages.SystemErrorMessage, nameof(commmand.ImageAlt));
        }

    }

    public EditSiteService GetForEdit(int id) =>
        _siteServiceepository.GetForEdit(id);
}

using Shared.Application;
using Shared.Application.Services;
using Shared;
using Site.Application.Contract.SiteSettingApplication.Command;
using Site.Domain.SiteSettingAgg;

namespace Site.Application.Services;

internal class SiteSettingApplication : ISiteSettingApplication
{
    private readonly ISiteSettingRepository _siteSettingRepository;
    private readonly IFileService _fileService;

    public SiteSettingApplication(ISiteSettingRepository siteSettingRepository, IFileService fileService)
    {
        _siteSettingRepository = siteSettingRepository;
        _fileService = fileService;
    }

    public UbsertSiteSetting GetForUbsert() =>
        _siteSettingRepository.GetForUbsert();

    public OperationResult Ubsert(UbsertSiteSetting command)
    {
        SiteSetting site = _siteSettingRepository.GetSingle();
        string logoName = site.LogoName;
        string oldLogoName = site.LogoName;
        if (command.LogoFile != null)
        {
            if (!command.LogoFile.IsImage()) return new(false, ValidationMessages.ImageErrorMessage, nameof(command.LogoFile));
            logoName = _fileService.UploadImage(command.LogoFile, FileDirectories.SiteImageFolder);
            if (logoName == "")
                return new(false, ValidationMessages.ImageErrorMessage, nameof(command.LogoFile));
            _fileService.ResizeImage(logoName, FileDirectories.SiteImageDirectory300, 300);
        }
        string favIconName = site.FavIcon;
        string oldfavIconName = site.FavIcon;
        if (command.FavIconFile != null)
        {
            if (!command.FavIconFile.IsImage()) return new(false, ValidationMessages.ImageErrorMessage, nameof(command.FavIconFile));
            favIconName = _fileService.UploadImage(command.FavIconFile, FileDirectories.SiteImageFolder);
            if (logoName == "")
                return new(false, ValidationMessages.ImageErrorMessage, nameof(command.FavIconFile));
            _fileService.ResizeImage(favIconName, FileDirectories.SiteImageDirectory64, 64);
            _fileService.ResizeImage(favIconName, FileDirectories.SiteImageDirectory32, 32);
            _fileService.ResizeImage(favIconName, FileDirectories.SiteImageDirectory16, 16);
        }
        site.Edit(command.Instagram, command.WhatsApp, command.Telegram, command.Youtube, logoName,
            command.LogoAlt, favIconName, command.Enamad, command.SamanDehi, command.SeoBox,
            command.Android, command
            .IOS, command.FooterDescription, command.FooterTitle, command.Phone1, command.Phone2,
            command.Email1, command.Email2, command.Address, command.ContactDescription, command.AboutDescription, command.AboutTitle);
        if (_siteSettingRepository.Save())
        {
            if(command.LogoFile != null)
            {
                if (!string.IsNullOrEmpty(oldLogoName))
                {
                    _fileService.DeleteImage($"{FileDirectories.SiteImageDirectory}{oldLogoName}");
                    _fileService.DeleteImage($"{FileDirectories.SiteImageDirectory300}{oldLogoName}");
                }
            }
            if (command.FavIconFile != null)
            {
                if (!string.IsNullOrEmpty(oldfavIconName))
                {
                    _fileService.DeleteImage($"{FileDirectories.SiteImageDirectory}{oldfavIconName}");
                    _fileService.DeleteImage($"{FileDirectories.SiteImageDirectory64}{oldfavIconName}");
                    _fileService.DeleteImage($"{FileDirectories.SiteImageDirectory32}{oldfavIconName}");
                    _fileService.DeleteImage($"{FileDirectories.SiteImageDirectory16}{oldfavIconName}");
                }
            }
            return new(true);
        }
        else
        {
            if (command.LogoFile != null)
            {
                    _fileService.DeleteImage($"{FileDirectories.SiteImageDirectory}{logoName}");
                    _fileService.DeleteImage($"{FileDirectories.SiteImageDirectory300}{logoName}");
                
            }
            if (command.FavIconFile != null)
            {
                    _fileService.DeleteImage($"{FileDirectories.SiteImageDirectory}{favIconName}");
                    _fileService.DeleteImage($"{FileDirectories.SiteImageDirectory64}{favIconName}");
                    _fileService.DeleteImage($"{FileDirectories.SiteImageDirectory32}{favIconName}");
                    _fileService.DeleteImage($"{FileDirectories.SiteImageDirectory16}{favIconName}");
            }
            return new(false,ValidationMessages.SystemErrorMessage,nameof(command.Instagram));
        }
    }
}
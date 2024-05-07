using PostModule.Application.Contract.PostSettingApplication.Command;
using PostModule.Domain.SettingAgg;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Services
{
    internal class PostSettingApplication : IPostSettingApplication
    {
        private readonly IPostSettingRepository _postSettingRepository;
        public PostSettingApplication(IPostSettingRepository postSettingRepository)
        {
            _postSettingRepository = postSettingRepository;
        }
        public UbsertPostSetting GetForUbsert() =>
            _postSettingRepository.GetForUbsert();

        public OperationResult Ubsert(UbsertPostSetting command)
        {
            PostSetting setting = _postSettingRepository.GetSingle();
            setting.Edit(command.PackageTitle, command.PackageDescription,command.ApiDescription);
            if (_postSettingRepository.Save()) return new(true);
            return new OperationResult(false,ValidationMessages.SystemErrorMessage,nameof(command.PackageTitle));
        }
    }
}

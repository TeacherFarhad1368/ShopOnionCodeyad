using PostModule.Application.Contract.PostSettingApplication.Command;
using Shared.Domain;

namespace PostModule.Domain.SettingAgg
{
    public interface IPostSettingRepository : IRepository<int, PostSetting>
    {
        UbsertPostSetting GetForUbsert();
        PostSetting GetSingle();
    }
}

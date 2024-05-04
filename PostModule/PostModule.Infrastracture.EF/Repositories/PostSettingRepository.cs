using Shared.Infrastructure;
using PostModule.Domain.SettingAgg;
using PostModule.Application.Contract.PostSettingApplication.Command;

namespace PostModule.Infrastracture.EF.Repositories;

internal class PostSettingRepository : Repository<int, PostSetting>, IPostSettingRepository
{
    private readonly Post_Context _context;
    public PostSettingRepository(Post_Context context) : base(context)
    {
        _context = context;
    }

    public UbsertPostSetting GetForUbsert()
    {
        var s = GetSingle();
        return new()
        {
            PackageDescription = s.PackageDescription,
            PackageTitle = s.PackageTitle
        };
    }

    public PostSetting GetSingle()
    {
        var setting = _context.PostSettings.SingleOrDefault();
        if(setting == null)
        {
            setting = new("", "");
            Create(setting);
        }
        return setting;
    }
}
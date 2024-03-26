using Shared.Application;
using Shared.Domain.Enum;
using Site.Application.Contract.BanerApplication.Query;
using Site.Domain.BanerAgg;

namespace Site.Query.Services;

internal class BanerQuery : IBanerQuery
{
    private readonly IBanerRepository _banerRepository;

    public BanerQuery(IBanerRepository banerRepository)
    {
        _banerRepository = banerRepository;
    }

    public List<BanerForAdminQueryModel> GetAllForAdmin()
    {
        return _banerRepository.GetAllQuery().Select(b => new BanerForAdminQueryModel
        {
            Active = b.Active,
            CreationDate = b.CreateDate.ToPersainDate(),
            Id  =b.Id,
            ImageName = $"{FileDirectories.BanerImageDirectory100}{b.ImageName}",
            State = b.State,
            ImageAlt = b.ImageAlt,
        }).ToList();
    }

    public List<BanerForUiQueryModel> GetForUi(int count, BanerState state)
    {
        throw new NotImplementedException();
    }
}
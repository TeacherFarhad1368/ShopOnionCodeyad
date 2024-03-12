using Shared.Domain.Enum;
using Site.Application.Contract.BanerApplication.Query;

namespace Site.Query.Services;

internal class BanerQuery : IBanerQuery
{
    public List<BanerForAdminQueryModel> GetAllForAdmin()
    {
        throw new NotImplementedException();
    }

    public List<BanerForUiQueryModel> GetForUi(int count, BanerState state)
    {
        throw new NotImplementedException();
    }
}
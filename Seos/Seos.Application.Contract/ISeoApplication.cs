
using Shared.Domain.Enum;

namespace Seos.Application.Contract
{
    public interface ISeoApplication
    {
        bool UbsertSeo(CreateSeo command);
        CreateSeo GetSeoForEdit(int ownerId, WhereSeo where);
    }
}

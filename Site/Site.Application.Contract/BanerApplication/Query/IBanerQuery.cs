using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.BanerApplication.Query
{
    public interface IBanerQuery
    {
        List<BanerForAdminQueryModel> GetAllForAdmin();
        List<BanerForUiQueryModel> GetForUi(int count, BanerState state);   
    }
}

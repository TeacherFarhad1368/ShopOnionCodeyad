using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.Admin.Seo
{
    public interface ISeoAdminQuery
    {
        string GetAdminSeoTitle(WhereSeo where, int ownerId);
    }
}

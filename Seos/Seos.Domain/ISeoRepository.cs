using Seos.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Shared.Domain.Enum;
using Shared.Domain;

namespace Seos.Domain
{
    public interface ISeoRepository : IRepository<int,Seo>
    {
        CreateSeo GetSeoForUbsert(int ownerId, WhereSeo where);
        Seo GetSeo(int ownerId, WhereSeo where);
        Seo GetSeoForUi(int ownerId, WhereSeo where,string title);
    }
}

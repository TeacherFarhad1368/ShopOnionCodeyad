using Seos.Application.Contract;
using Seos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Shared.Domain.Enum;
using Shared.Infrastructure;

namespace Seos.Infrastructure
{
    internal class SeoRepository : Repository<int,Seo> , ISeoRepository
    {
        private readonly Seo_Context _context;
        public SeoRepository(Seo_Context context): base(context)
        {
            _context = context;
        }
       
        public Seo GetSeo(int ownerId, WhereSeo where)
        {
            return _context.Seos.FirstOrDefault(s => s.OwnerId == ownerId && s.Where == where);
        }

        public CreateSeo GetSeoForUbsert(int ownerId, WhereSeo where)
        {
            var seo = _context.Seos.FirstOrDefault(s => s.OwnerId == ownerId && s.Where == where);
            if (seo == null)
                return new()
                {
                    OwnerId = ownerId,
                    Where = where
                };

            return new()
            {
                OwnerId = seo.OwnerId,
                Canonical = seo.Canonical,
                IndexPage = seo.IndexPage,
                MetaDescription = seo.MetaDescription,
                MetaKeyWords = seo.MetaKeyWords,
                MetaTitle = seo.MetaTitle,
                Schema = seo.Schema,
                Where = seo.Where
            };
        }

    }
}

using Microsoft.EntityFrameworkCore;
using Site.Application.Contract.SiteSettingApplication.Command;
using Site.Domain.SiteSettingAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure.Services
{
    internal class SiteSettingRepository : ISiteSettingRepository
    {
        private readonly SiteContext _context;

        public SiteSettingRepository(SiteContext context)
        {
            _context = context;
        }

        public UbsertSiteSetting GetForUbsert()
        {
            var site = GetSingle();
            return new()
            {
                AboutDescription = site.AboutDescription,
                AboutTitle = site.AboutTitle,
                Address = site.Address,
                Android = site.Android,
                LogoAlt = site.LogoAlt,
                WhatsApp = site.WhatsApp,
                ContactDescription = site.ContactDescription,
                Email1 = site.Email1,
                Email2 = site.Email2,
                Enamad = site.Enamad,
                FavIcon = site.FavIcon,
                FavIconFile = null,
                FooterDescription = site.FooterDescription,
                FooterTitle = site.FooterTitle,
                Instagram = site.Instagram,
                IOS = site.IOS,
                LogoFile = null,
                LogoName = site.LogoName,
                Phone1 = site.Phone1,
                Phone2 = site.Phone2,
                SamanDehi = site.SamanDehi,
                SeoBox = site.SeoBox,
                Telegram = site.Telegram,
                Youtube = site.Youtube
            };
        }

        public SiteSetting GetSingle()
        {
            SiteSetting site = _context.SiteSettings.SingleOrDefault();
            if(site == null)
            {
                site = new();
                _context.SiteSettings.Add(site);
                Save();
            }
            return site;    
        }
        public bool Save() =>
            _context.SaveChanges() >= 0 ? true : false;
    }
}

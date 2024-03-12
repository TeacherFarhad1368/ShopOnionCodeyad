using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.SiteSettingAgg
{
    public class SiteSetting
    {
        public SiteSetting()
        {
            Instagram = "";
            WhatsApp = "";
            Telegram = "";
            Youtube = "";
            LogoName = "";
            LogoAlt = "";
            FavIcon = "";
            Enamad = "";
            SamanDehi = "";
            SeoBox = "";
            Android = "";
            IOS = "";
            FooterDescription = "";
            Phone1 = "";
            Phone2 = "";
            Email1 = "";
            Email2 = "";
            Address = "";
            ContactDescription = "";
            AboutDescription = "";
            AboutTitle = "";
        }

        public void Edit(string? instagram, string? whatsApp, string? telegram, string? youtube,
            string? logoName, string? logoAlt, string? favIcon, string? enamad, string? samanDehi, 
            string? seoBox, string? android, string? iOS, string? footerDescription, string? footerTitle,
            string? phone1, string? phone2, string? email1, string? email2, 
            string? address, string? contactDescription, string? aboutDescription, string? aboutTitle)
        {
            Instagram = instagram;
            WhatsApp = whatsApp;
            Telegram = telegram;
            Youtube = youtube;
            LogoName = logoName;
            LogoAlt = logoAlt;
            FavIcon = favIcon;
            Enamad = enamad;
            SamanDehi = samanDehi;
            SeoBox = seoBox;
            Android = android;
            IOS = iOS;
            FooterDescription = footerDescription;
            FooterTitle = footerTitle;
            Phone1 = phone1;
            Phone2 = phone2;
            Email1 = email1;
            Email2 = email2;
            Address = address;
            ContactDescription = contactDescription;
            AboutDescription = aboutDescription;
            AboutTitle = aboutTitle;
        }

        public string? Instagram { get; private set; }
        public string? WhatsApp { get; private set; }
        public string? Telegram { get; private set; }
        public string? Youtube { get; private set; }


        public string? LogoName { get; private set; }
        public string? LogoAlt { get; private set; }
        public string? FavIcon { get; private set; }


        public string? Enamad { get; private set; }
        public string? SamanDehi { get; private set; }
        public string? SeoBox { get; private set; }
        public string? Android { get; private set; }
        public string? IOS { get; private set; }
        public string? FooterDescription { get; private set; }
        public string? FooterTitle { get; private set; }



        public string? Phone1 { get; private set; }
        public string? Phone2 { get; private set; }
        public string? Email1 { get; private set; }
        public string? Email2 { get; private set; }
        public string? Address { get; private set; }
        public string? ContactDescription { get; private set; }



        public string? AboutDescription { get; private set; }
        public string? AboutTitle { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.SiteSettingApplication.Command
{
    public interface ISiteSettingApplication
    {
    }
    public class UbserSiteSetting
    {
        public string? Instagram { get; set; }
        public string? WhatsApp { get; set; }
        public string? Telegram { get; set; }
        public string? Youtube { get; set; }


        public string? LogoName { get; set; }
        public string? LogoAlt { get; set; }
        public string? FavIcon { get; set; }


        public string? Enamad { get; set; }
        public string? SamanDehi { get; set; }
        public string? SeoBox { get; set; }
        public string? Android { get; set; }
        public string? IOS { get; set; }
        public string? FooterDescription { get; set; }
        public string? FooterTitle { get; set; }



        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public string? Address { get; set; }
        public string? ContactDescription { get; set; }



        public string? AboutDescription { get; set; }
        public string? AboutTitle { get; set; }
    }
}

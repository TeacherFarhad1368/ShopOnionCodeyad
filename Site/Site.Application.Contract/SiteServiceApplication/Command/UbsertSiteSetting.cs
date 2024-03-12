using Microsoft.AspNetCore.Http;
using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Site.Application.Contract.SiteServiceApplication.Command
{
    public class UbsertSiteSetting
    {
        [Display(Name = "لینک پیج اینستاگرام")]
        [MaxLength(800, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string? Instagram { get; set; }
        [Display(Name = "لینک واتس اپ")]
        [MaxLength(800, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string? WhatsApp { get; set; }
        [Display(Name = "لینک تلگرام")]
        [MaxLength(800, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string? Telegram { get; set; }
        [Display(Name = "لینک پیج یوتیوب")]
        [MaxLength(800, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string? Youtube { get; set; }
        [Display(Name = "لوگو")]
        public IFormFile? LogoFile { get; set; }
        public string? LogoName { get; set; }
        [Display(Name = "Alt لوگو")]
        [MaxLength(250, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string? LogoAlt { get; set; }
        [Display(Name = "فاو آیکون")]
        public IFormFile? FavIconFile { get; set; }
        public string? FavIcon { get; set; }
        [Display(Name = "لینک اینماد")]
        [MaxLength(250, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string? Enamad { get; set; }
        [Display(Name = "لینک ساماندهی")]
        [MaxLength(800, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string? SamanDehi { get; set; }
        [Display(Name = "Seo Box")]
        public string? SeoBox { get; set; }
        [Display(Name = "لینک دانلود اپلیکیشن اندروید")]
        [MaxLength(800, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string? Android { get; set; }
    }
}

﻿using Microsoft.AspNetCore.Http;
using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Site.Application.Contract.MenuApplication.Command
{
    public class UbsertMenu
    {
        [Display(Name = "تصویر (برای سر منو های صفحه اصلی که زیر منو دارند و دراپ داون طور هستند و در منو های وبلاگی که زیر منو دارن میتوانید برای زیر منو هاش عکس وارد کنید .)")]
        public IFormFile? ImageFile { get; set; }
        [Display(Name = "Alt تصویر")]
        [MaxLength(250, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string? ImageAlt { get; set; }
        [Display(Name = "شماره برای ترتیب بندی")]
        public int Number { get; set; } = 0;
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(250, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string Title { get; set; }
        [Display(Name = "لینک مقصد (برای منو هایی که زیر منو دارند # وارد کنید )")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(250, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string Url { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Site.Application.Contract.BanerApplication.Command
{
    public class EditBaner
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile? ImageFile { get; set; }
        [Display(Name = "Alt تصویر")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(250, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string ImageAlt { get; set; }
        [Display(Name = "لینک مقصد")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(900, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string Url { get; set; }
    }
}

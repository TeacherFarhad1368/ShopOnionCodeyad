using Microsoft.AspNetCore.Http;
using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Blogs.Application.Contract.BlogCategoryApplication.Command
{
    public class CreateBlogCategory
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(250, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string Title { get; set; }
        [Display(Name = "لینک سربرگ")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(300, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string Slug { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile? ImageFile { get; set; }
        [Display(Name = "Alt تصویر")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(150, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string ImageAlt { get; set; }
        public int Parent { get; set; }
    }
}

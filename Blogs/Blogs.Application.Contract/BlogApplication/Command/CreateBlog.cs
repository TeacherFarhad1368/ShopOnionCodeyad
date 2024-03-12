using Microsoft.AspNetCore.Http;
using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Blogs.Application.Contract.BlogApplication.Command
{
    public class CreateBlog
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(250, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string Title { get; set; }
        [Display(Name = "لینک سربرگ")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(300, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string Slug { get; set; }
        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(600, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string ShortDescription { get; set; }
        [Display(Name = "توضیح")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Text { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile? ImageFile { get; set; }
        [Display(Name = "Alt تصویر")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(150, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string ImageAlt { get; set; }
        [Display(Name = "سر گروه")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public int CategoryId { get; set; }
        [Display(Name = "زیرگروه")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public int SubCategoryId { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public long UserId { get; set; }
        [Display(Name = "نویسنده")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(255, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string Writer { get; set; }
    }
}

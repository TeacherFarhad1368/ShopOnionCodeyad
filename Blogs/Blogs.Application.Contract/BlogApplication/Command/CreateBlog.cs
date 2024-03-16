using Microsoft.AspNetCore.Http;
using Shared.Application;
using Shared.Application.BaseCommands;
using System.ComponentModel.DataAnnotations;

namespace Blogs.Application.Contract.BlogApplication.Command
{
    public class CreateBlog : Text_ShortDescription_Title_Slug_Image_ImageAlt
	{  
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

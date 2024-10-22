using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.Application.BaseCommands
{
    public class Text_ShortDescription_Title_Slug_Image_ImageAlt : Text_ShortDescription_Title_Slug
	{
		[Display(Name = "تصویر")]
		public IFormFile? ImageFile { get; set; }
		[Display(Name = "Alt تصویر")]
		[Required(ErrorMessage = ValidationMessages.RequiredMessage)]
		[MaxLength(150, ErrorMessage = ValidationMessages.MaxLengthMessage)]
		public string ImageAlt { get; set; }
	}
}

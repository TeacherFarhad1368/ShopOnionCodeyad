using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.Application.BaseCommands
{
    public class Image_ImageAlt
	{
		[Display(Name = "تصویر")]
		public IFormFile? ImageFile { get; set; }
		[Display(Name = "Alt تصویر")]
		[Required(ErrorMessage = ValidationMessages.RequiredMessage)]
		[MaxLength(150, ErrorMessage = ValidationMessages.MaxLengthMessage)]
		public string ImageAlt { get; set; }
	}
}

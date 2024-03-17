using Microsoft.AspNetCore.Http;
using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Site.Application.Contract.ImageSiteApplication.Command
{
	public class CreateImageSite
	{
		[Display(Name = "تصویر")]
		[Required(ErrorMessage = ValidationMessages.ImageErrorMessage)]
		public IFormFile ImageFile { get; set; }
		[Display(Name = "عنوان")]
		[Required(ErrorMessage = ValidationMessages.RequiredMessage)]
		[MaxLength(250, ErrorMessage = ValidationMessages.MaxLengthMessage)]
		public string Title { get; set; }
	}
}

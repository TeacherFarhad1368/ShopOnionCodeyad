using System.ComponentModel.DataAnnotations;

namespace Shared.Application.BaseCommands
{
    public class Title_Slug_Image_ImageAlt : Image_ImageAlt
	{
		[Display(Name = "عنوان")]
		[Required(ErrorMessage = ValidationMessages.RequiredMessage)]
		[MaxLength(250, ErrorMessage = ValidationMessages.MaxLengthMessage)]
		public string Title { get; set; }
		[Display(Name = "لینک سربرگ")]
		[Required(ErrorMessage = ValidationMessages.RequiredMessage)]
		[MaxLength(300, ErrorMessage = ValidationMessages.MaxLengthMessage)]
		public string Slug { get; set; }
	}
}

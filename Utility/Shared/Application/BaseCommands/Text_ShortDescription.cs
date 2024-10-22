using System.ComponentModel.DataAnnotations;

namespace Shared.Application.BaseCommands
{
    public class Text_ShortDescription
	{
		[Display(Name = "توضیح مختصر")]
		[Required(ErrorMessage = ValidationMessages.RequiredMessage)]
		[MaxLength(600, ErrorMessage = ValidationMessages.MaxLengthMessage)]
		public string ShortDescription { get; set; }
		[Display(Name = "توضیح")]
		[Required(ErrorMessage = ValidationMessages.RequiredMessage)]
		public string Text { get; set; }
	}
}

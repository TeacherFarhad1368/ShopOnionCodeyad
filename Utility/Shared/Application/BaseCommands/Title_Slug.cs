using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.BaseCommands
{
	public class Title_Slug
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
	public class Image_ImageAlt
	{
		[Display(Name = "تصویر")]
		public IFormFile? ImageFile { get; set; }
		[Display(Name = "Alt تصویر")]
		[Required(ErrorMessage = ValidationMessages.RequiredMessage)]
		[MaxLength(150, ErrorMessage = ValidationMessages.MaxLengthMessage)]
		public string ImageAlt { get; set; }
	}
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
	public class Text_ShortDescription_Title_Slug : Title_Slug
	{
		[Display(Name = "توضیح مختصر")]
		[Required(ErrorMessage = ValidationMessages.RequiredMessage)]
		[MaxLength(600, ErrorMessage = ValidationMessages.MaxLengthMessage)]
		public string ShortDescription { get; set; }
		[Display(Name = "توضیح")]
		[Required(ErrorMessage = ValidationMessages.RequiredMessage)]
		public string Text { get; set; }
	}
	public class Text_ShortDescription_Title_Slug_Image_ImageAlt : Text_ShortDescription_Title_Slug
	{
		[Display(Name = "تصویر")]
		public IFormFile? ImageFile { get; set; }
		[Display(Name = "Alt تصویر")]
		[Required(ErrorMessage = ValidationMessages.RequiredMessage)]
		[MaxLength(150, ErrorMessage = ValidationMessages.MaxLengthMessage)]
		public string ImageAlt { get; set; }
	}
	public class Text_ShortDescription_Image_ImageAlt : Text_ShortDescription
	{
		[Display(Name = "تصویر")]
		public IFormFile? ImageFile { get; set; }
		[Display(Name = "Alt تصویر")]
		[Required(ErrorMessage = ValidationMessages.RequiredMessage)]
		[MaxLength(150, ErrorMessage = ValidationMessages.MaxLengthMessage)]
		public string ImageAlt { get; set; }
	}
}

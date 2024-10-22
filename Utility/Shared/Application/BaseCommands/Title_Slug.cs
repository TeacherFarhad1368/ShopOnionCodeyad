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
}

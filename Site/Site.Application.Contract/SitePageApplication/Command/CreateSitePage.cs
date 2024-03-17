using Shared.Application;
using Shared.Application.BaseCommands;
using System.ComponentModel.DataAnnotations;

namespace Site.Application.Contract.SitePageApplication.Command
{
	public class CreateSitePage : Title_Slug
	{
		[Display(Name = "توضیح")]
		[Required(ErrorMessage = ValidationMessages.RequiredMessage)]
		public string Text { get; set; }
	}
}

using Shared.Application;
using Shared.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Site.Application.Contract.MenuApplication.Command
{
    public class CreateMenu : UbsertMenu
    {
        [Display(Name = "نوع منو")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public MenuStatus Status { get; set; }
       
    }
}

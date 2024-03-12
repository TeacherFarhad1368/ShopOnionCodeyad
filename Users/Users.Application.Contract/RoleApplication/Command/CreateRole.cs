using Shared.Application;
using Shared.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Users.Application.Contract.RoleApplication.Command
{
    public class CreateRole
    {
        [Display(Name ="عنوان")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(255 , ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string Title { get; set; }
       
    }
}

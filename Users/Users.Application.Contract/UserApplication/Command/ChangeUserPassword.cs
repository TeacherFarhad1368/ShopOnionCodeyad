using Shared.Application;
using Shared.Application.Validations;
using System.ComponentModel.DataAnnotations;

namespace Users.Application.Contract.UserApplication.Command
{
    public class ChangeUserPassword
    {
        public int UserId { get; set; }
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(10, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        [MinLength(5, ErrorMessage = ValidationMessages.MinLengthMessage)]
        public string OldPassword { get; set; }
        [Display(Name = "کلمه عبور جدید")]
        [PasswordValidation(ErrorMessage = ValidationMessages.PasswordErrorMessage)]
        public string NewPassword { get; set; }
        [Display(Name = "تکرار کلمه عبور جدید")]
        [PasswordValidation(ErrorMessage = ValidationMessages.PasswordErrorMessage)]
        [Compare("NewPassword", ErrorMessage = ValidationMessages.PasswordCompare)]
        public string ReNewPassword { get; set; }
    }
}

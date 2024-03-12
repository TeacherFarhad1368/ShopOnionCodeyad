using Microsoft.AspNetCore.Http;
using Shared.Application;
using Shared.Application.Validations;
using Shared.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Users.Application.Contract.UserApplication.Command
{
    public class EditUserByUser
    {
        public int Id { get; set; }
        [Display(Name = "نام کامل")]
        [MaxLength(255, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string? FullName { get; set; }
        [Display(Name = "شماره همراه")]
        [MobileValidation(ErrorMessage = ValidationMessages.MobileErrorMessage)]
        public string Mobile { get; set; }
        [Display(Name = "ایمیل")]
        [MaxLength(255, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string? Email { get; set; }
        public string AvatarName { get; set; }
        [Display(Name = "تصویرکاربری")]
        public IFormFile? AvatarFile { get; set; }
        [Display(Name = "جنسیت")]
        public Gender UserGender { get; set; }
    }
}

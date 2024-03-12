using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Emails.Application.Contract.SensEmailApplication.Command
{
    public class CreateSendEmail
    {
        [Display(Name = "متن ایمیل")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Text { get; set; }
        [Display(Name = "عنوان ایمیل")]
        [MaxLength(250,ErrorMessage = ValidationMessages.MaxLengthMessage)]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Title { get; set; }
    }
}

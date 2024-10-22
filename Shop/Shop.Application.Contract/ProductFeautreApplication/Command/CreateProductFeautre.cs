using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Contract.ProductFeautreApplication.Command
{
    public class CreateProductFeautre
    {
        public int ProductId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(250, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string Title { get; set; }
        [Display(Name = "مقدار")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Value { get; set; }
    }
}

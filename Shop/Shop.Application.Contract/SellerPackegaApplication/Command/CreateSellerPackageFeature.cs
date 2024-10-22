using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Contract.SellerPackegaApplication.Command
{
    public class CreateSellerPackageFeature
    {
        public int SellerPackageId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(355, ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string Title { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Description { get; set; }
    }
}

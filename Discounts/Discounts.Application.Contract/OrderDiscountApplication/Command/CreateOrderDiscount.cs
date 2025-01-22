using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Discounts.Application.Contract.OrderDiscountApplication.Command;

public class CreateOrderDiscount
{
    [Display(Name = "درصد تخفیف")]
    public int Percent { get; set; }
    [Display(Name = "عنوان تخفیف (مناسبت)")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    [MaxLength(355,ErrorMessage = ValidationMessages.MaxLengthMessage)]
    public string Title { get; set; }
    [Display(Name = "کد تخفیف")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    [MaxLength(10, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    [MinLength(4, ErrorMessage = ValidationMessages.MinLengthMessage)]
    public string Code { get; set; }
    [Display(Name = "تعداد تخفیف")]
    public int Count { get; set; }
    [Display(Name = "تاریخ شروع")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    public string StartDate { get; set; }
    [Display(Name = "تاریخ پایان")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    public string EndDate { get; set; }
}
using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Discounts.Application.Contract.ProductDiscountApplication.Command;

public class CreateProductSellDiscount
{
    [Display(Name = "انتخاب محصول")]
    public int ProductSellId { get; set; }
    [Display(Name = "تاریخ شروع")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    public string StartDate { get; set; }
    [Display(Name = "تاریخ پایان")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    public string EndDate { get; set; }
    [Display(Name = "درصد تخفیف")]
    public int Percent { get; set; }
}
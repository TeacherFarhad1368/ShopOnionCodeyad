﻿using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Discounts.Application.Contract.ProductDiscountApplication.Command;

public class CreateProductDiscount
{
    [Display(Name = "انتخاب محصول")]
    public int ProductId { get; set; }
    [Display(Name = "تاریخ شروع")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    public DateTime StartDate { get; set; }
    [Display(Name = "تاریخ پایان")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    public DateTime EndDate { get; set; }
    [Display(Name = "درصد تخفیف")]
    public int Percent { get; set; }
}

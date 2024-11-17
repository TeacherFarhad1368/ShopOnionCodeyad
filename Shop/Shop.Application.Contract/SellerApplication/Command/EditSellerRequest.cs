using Microsoft.AspNetCore.Http;
using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Contract.SellerApplication.Command;

public class EditSellerRequest : RequestSeller
{
    public int Id { get; set; }
    public string ImageName { get; set; }
    public string ImageAcceptName { get; set; }
    [Display(Name = "نام فروشگاه")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    [MaxLength(355, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    public string Title { get; set; }
    [Display(Name = "انتخاب استان")]
    public int StateId { get; set; }
    [Display(Name = "انتخاب شهر")]
    public int CityId { get; set; }
    [Display(Name = "جزییات آدرس")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    public string Address { get; set; }
    [Display(Name = "لینک نقشه گوگل")]
    public string? GoogleMapUrl { get; set; }
    [Display(Name = "تصویر مجوز فروشگاه")]
    public IFormFile? ImageAccept { get; set; }
    [Display(Name = "تصویر")]
    public IFormFile? ImageFile { get; set; }
    [Display(Name = "Alt تصویر")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    [MaxLength(355, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    public string ImageAlt { get; set; }
    [Display(Name = "لینک چت وانس اپ")]
    [MaxLength(355, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    public string? WhatsApp { get; set; }
    [Display(Name = "لینک پیج تلگرام")]
    [MaxLength(355, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    public string? Telegram { get; set; }
    [Display(Name = "لینک پیج اینستاگرام")]
    [MaxLength(355, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    public string? Instagram { get; set; }
    [Display(Name = "شماره موبایل")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    [MaxLength(11, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    [MinLength(11, ErrorMessage = ValidationMessages.MinLengthMessage)]
    public string Phone1 { get; set; }
    [Display(Name = "شماره تماس 2")]
    [MaxLength(11, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    [MinLength(11, ErrorMessage = ValidationMessages.MinLengthMessage)]
    public string? Phone2 { get; set; }
    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    [MaxLength(255, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    public string? Email { get; set; }
}

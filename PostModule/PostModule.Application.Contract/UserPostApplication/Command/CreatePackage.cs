using Microsoft.AspNetCore.Http;
using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace PostModule.Application.Contract.UserPostApplication.Command;

public class CreatePackage
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    [MaxLength(355 , ErrorMessage = ValidationMessages.MaxLengthMessage)]
    public string Title { get;set; }
    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    public string Description { get;set; }
    [Display(Name = "تعداد درخواست")]
    public int Count { get;set; }
    [Display(Name = "قیمت")]
    public int Price { get;set; }
    [Display(Name = "تصویر")]
    public IFormFile? ImageFile { get; set; }
    [Display(Name = "alt تصویر")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    [MaxLength(150, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    public string ImageAlt { get; set; }
}

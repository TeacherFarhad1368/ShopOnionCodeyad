using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Contract.OrderApplication.Command;

public class CreateOrderAddress
{
    [Display(Name = "استان")]
    public int StateId { get; set; }
    [Display(Name = "شهر")]
    public int CityId { get; set; }
    [Display(Name = "جزییات آدرس")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    [MaxLength(500, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    public string AddressDetail { get; set; }
    [Display(Name = "کد پستی")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    [MaxLength(10, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    [MinLength(10, ErrorMessage = ValidationMessages.MinLengthMessage)]
    public string PostalCode { get; set; }
    [Display(Name = "شماره تماس تحویل گیرنده *")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    [MaxLength(11, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    [MinLength(11, ErrorMessage = ValidationMessages.MinLengthMessage)]
    public string Phone { get; set; }
    [Display(Name = "نام تحویل گیرنده *")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    [MaxLength(255, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    public string FullName { get; set; }
    [Display(Name = "کد ملی")]
    [MaxLength(10, ErrorMessage = ValidationMessages.MaxLengthMessage)]
    [MinLength(10, ErrorMessage = ValidationMessages.MinLengthMessage)]
    public string? IranCode { get; set; }
}
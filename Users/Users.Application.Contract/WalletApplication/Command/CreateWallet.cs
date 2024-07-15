using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Users.Application.Contract.WalletApplication.Command;

public class CreateWallet
{
    public int UserId { get; set; }
    [Display(Name = "مبلغ (تومان)")]
    public int Price { get; set; }
    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
    public string Description { get; set; }
}

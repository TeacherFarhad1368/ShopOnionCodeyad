using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Discounts.Application.Contract.OrderDiscountApplication.Command;

public class EditOrderDiscount : CreateOrderDiscount
{
    public int Id { get; set; }
}
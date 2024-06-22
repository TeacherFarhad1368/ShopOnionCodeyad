using Shared.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Transactions.Application.Contract
{
    public class CreateTransaction
    {
        public int UserId { get; set; }
        [Display(Name ="مبلغ (تومان)")]
        public int Price { get; set; }
        [Display(Name = "انتخاب درگاه")]
        public TransactionPortal Portal { get; set; }
    }
}

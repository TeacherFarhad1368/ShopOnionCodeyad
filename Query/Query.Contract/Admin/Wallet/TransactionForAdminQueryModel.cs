using Shared.Domain.Enum;

namespace Query.Contract.Admin.Wallet;

public class TransactionForAdminQueryModel
{
    public long Id { get; set; }
    public int Price { get; set; }
    public string RefId { get; set; }
    public TransactionPortal Portal { get; set; }
    public TransactionStatus Status { get; set; }
    public TransactionFor TransactionFor { get; set; }
    public int OwnerId { get; set; }
    public string CretionDate { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
}
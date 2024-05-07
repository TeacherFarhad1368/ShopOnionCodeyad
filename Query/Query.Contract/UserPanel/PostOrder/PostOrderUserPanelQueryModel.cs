using Shared.Domain.Enum;

namespace Query.Contract.UserPanel.PostOrder;

public class PostOrderUserPanelQueryModel
{
   
    public int Id { get; set; }
    public int PackageId { get; set; }
    public string PackageTitle { get; set; }
    public string PackageImage { get; set; }
    public int Count { get; set; }
    public string Date { get; set; }
    public int transactionId { get; set; }
    public string TransactionRef { get; set; }
    public PostOrderStatus Status { get; set; }
}
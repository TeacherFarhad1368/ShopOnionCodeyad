using Shared;

namespace Query.Contract.Admin.Wallet;

public class TransactionsForAdminPaging : BasePaging
{
    public int UserId { get; set; }
    public string PageTitle { get; set; }
    public string Filter { get; set; }
    public int TransactiionSuccessSum { get; set; }
    public OrderingWalletSearch OrderBy { get; set; }
    public TransactionPortalSearch Portal { get; set; }
    public TransactionForSearch TransactionFor { get; set; }
    public TransactionStatusSearch Status { get; set; }
    public List<TransactionForAdminQueryModel> Transactions { get; set; }
}
public enum TransactionPortalSearch
{
    همه,
    زرین_پال
}
public enum TransactionForSearch
{
    همه,
    کیف_پول,
    فاکتور
}
public enum TransactionStatusSearch
{
    همه,
    نا_موفق,
    موفق
}
using Shared;

namespace Query.Contract.Admin.Wallet;

public class UserWalletForAdminPaging : BasePaging
{
    public int UserId { get; set; }
    public int WalletAmount { get; set; }
    public string UserName { get; set; }
    public OrderingWalletSearch OrderBy { get; set; }
    public WalletWhySerch WalletWhy { get; set; }
    public WalletTypeSearch Type { get; set; }
    public List<UserWalletAdminQueryModel> Wallets { get; set; }
}
public enum WalletWhySerch
{
    همه,
    توسط_ادمین,
    پرداخت_از_درگاه,
    خرید_از_سایت,
    بازگشت_فاکتور,
    کارت_هدیه
}
public enum WalletTypeSearch
{
    همه,
    برداشت,
    واریز
}
using Shared.Domain;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.UserAgg;

namespace Users.Domain.WalletAgg;
public class Wallet : BaseEntityCreate<int>
{
    public int UserId { get; private set; }
    public int Price { get; private set; }
    public WalletType Type { get; private set; }
    public WalletWhy WalletWhy { get; private set; } 
    public bool IsPay { get; private set; }
    public string Description { get; private set; }
    public User User { get; private set; }
    public Wallet()
    {
        User = new();
    }

    public Wallet(int userId, int price, WalletType type,WalletWhy walletWhy, bool isPay,string description)
    {
        UserId = userId;
        Price = price;
        Type = type;
        IsPay = isPay;
        Description = description;
        WalletWhy = walletWhy;
    }

    public static Wallet DepositByUser(int userId, int price, string description,WalletWhy walletWhy)
    {
        return new Wallet(userId, price, WalletType.واریز, walletWhy, false,description);
    }
    public void PaymentSuccess()
    {
        IsPay = true;
    }
    public static Wallet DepositByAdmin(int userId, int price, string description)
    {
        return new Wallet(userId,price,WalletType.واریز,WalletWhy.توسط_ادمین,true,description);  
    }
    public static Wallet DepositForReportOrderSeller(int userId, int price, string description)
    {
        return new Wallet(userId, price, WalletType.واریز, WalletWhy.بازگشت_ریز_فاکتور, true, description);
    }
    public static Wallet Withdrawall(int userId, int price, string description,WalletWhy walletWhy)
    {
        return new Wallet(userId, price, WalletType.برداشت, walletWhy, true, description);
    }

    public static Wallet DepositForPaymentOrderSeller(int userId, int price, string description)
    {
        return new Wallet(userId, price, WalletType.واریز, WalletWhy.پرداخت_ریز_فاکتور, true, description);
    }

    public static Wallet WithdrawForReportOrderSeller(int userId, int price, string description)
    {
        return new Wallet(userId, price, WalletType.برداشت, WalletWhy.بازگشت_ریز_فاکتور, true, description);
    }
}

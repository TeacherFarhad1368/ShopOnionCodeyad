﻿using Shared.Domain;
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
    public bool IsPay { get; private set; }
    public string Description { get; private set; }
    public User User { get; private set; }
    public Wallet()
    {
        User = new();
    }

    public Wallet(int userId, int price, WalletType type, bool isPay,string description)
    {
        UserId = userId;
        Price = price;
        Type = type;
        IsPay = isPay;
        Description = description;
    }

    public static Wallet DepositByUser(int userId, int price, string description)
    {
        return new Wallet(userId, price, WalletType.واریز, false,description);
    }
    public void PaymentSuccess()
    {
        IsPay = true;
    }
    public static Wallet DepositByAdmin(int userId, int price, string description)
    {
        return new Wallet(userId,price,WalletType.واریز,true,description);  
    }
    public static Wallet Withdrawall(int userId, int price, string description)
    {
        return new Wallet(userId, price, WalletType.برداشت, true, description);
    }
}

﻿using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.Wallet;
using Shared.Application;
using ShopBoloor.WebApplication.Utility;
using Users.Application.Contract.WalletApplication.Command;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.User
{
    [Area("Admin")]
    [PermissionChecker(Shared.Domain.Enum.UserPermission.کیف_پول)]
    public class WalletController : Controller
    {
        private readonly IAdminWalletQuery _adminWalletQuery;
        private readonly IWalletApplication _walletApplication;

        public WalletController(IAdminWalletQuery adminWalletQuery, IWalletApplication walletApplication)
        {
            _adminWalletQuery = adminWalletQuery;
            _walletApplication = walletApplication;
        }
        public IActionResult Wallets(int id,int pageId = 1, int take = 20,
            OrderingWalletSearch orderBy = OrderingWalletSearch.بر_اساس_تاریخ_از_آخر ,
            WalletTypeSearch type = WalletTypeSearch.همه, WalletWhySerch why = WalletWhySerch.همه)
        {
            var model = _adminWalletQuery.GetUserWalletsForAdmin(pageId, id, take, orderBy, type, why);
            return View(model);
        }
        public IActionResult Create(int id)
        {
            var model = new CreateWallet()
            {
                UserId = id
            };
            return PartialView("Create", model);
        }
        [HttpPost]
        public async Task<JsonResult> Create(int id,CreateWallet model)
        {
            OperationResult res = new(false);
            if (id != model.UserId)
                 res = new(false, "لطفا اطلاعات را درست وارد کنید .");
            else if (model.Price < 1000)
                res = new(false, "مبلغ باید بیشتر از 1,000 تومان باشد .");
            else
            res = await _walletApplication.DepositByAdminAsync(model);
            return new JsonResult(res);
        }
        public IActionResult Transaction(int userId,int take = 20,int pageId = 1,
            string filter = "", OrderingWalletSearch orderBy = OrderingWalletSearch.بر_اساس_تاریخ_از_آخر,
            TransactionForSearch transactionFor = TransactionForSearch.همه,
            TransactionStatusSearch status = TransactionStatusSearch.همه,
            TransactionPortalSearch portal = TransactionPortalSearch.همه)
        {
            var model = _adminWalletQuery.GetTransactionsForAdmin(pageId, userId, filter, take, orderBy, transactionFor, status, portal);
            return View(model);
        }
    }
}

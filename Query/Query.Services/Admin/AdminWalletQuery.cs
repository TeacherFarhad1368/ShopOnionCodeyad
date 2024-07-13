using Query.Contract.Admin.Wallet;
using Shared.Application;
using Transactions.Domain;
using Users.Domain.UserAgg.Repository;
using Users.Domain.WalletAgg;

namespace Query.Services.Admin
{
    internal class AdminWalletQuery : IAdminWalletQuery
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AdminWalletQuery(IUserRepository userRepository, IWalletRepository walletRepository, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        public TransactionsForAdminPaging GetTransactionsForAdmin(int pageId, int userId,
            string filter, int take, OrderingWalletSearch orderby
        , TransactionForSearch transactionFor, TransactionStatusSearch status, TransactionPortalSearch portal)
        {
            string title = "لیست تراکنش ها";
            if(userId > 0)
            {
                var user = _userRepository.GetById(userId);
                title = title + $" برای کاربر {user.FullName ?? ""} - {user.Mobile}";
            }
            IQueryable<Transaction> result = _transactionRepository.GetAllQuery().OrderByDescending(o => o.Id);
            if (userId > 0)
                result = result.Where(r => r.UserId == userId).OrderByDescending(o => o.Id);

            if(!string.IsNullOrEmpty(filter))
                result = result.Where(r=>r.RefId.Contains(filter)).OrderByDescending(o => o.Id);

            switch (orderby)
            {
                case OrderingWalletSearch.بر_اساس_تاریخ_از_اول:
                    result = result.OrderBy(o => o.Id);
                    break;
                case OrderingWalletSearch.بر_اساس_مبلغ_از_بالا_به_پایین:
                    result = result.OrderByDescending(o => o.Price);
                    break;
                case OrderingWalletSearch.بر_اساس_مبلغ_از_پایین_به_بالا:
                    result = result.OrderBy(o => o.Price);
                    break;
                default:
                    break;
            }
            switch (transactionFor)
            {
                case TransactionForSearch.کیف_پول:
                    result = result.Where(r => r.TransactionFor == Shared.Domain.Enum.TransactionFor.Wallet);
                    break;
                case TransactionForSearch.فاکتور:
                    result = result.Where(r => r.TransactionFor == Shared.Domain.Enum.TransactionFor.Order);
                    break;
                default:
                    break;
            }
            switch (status)
            {
                case TransactionStatusSearch.نا_موفق:
                    result = result.Where(r => r.Status == Shared.Domain.Enum.TransactionStatus.نا_موفق);
                    break;
                case TransactionStatusSearch.موفق:
                    result = result.Where(r => r.Status == Shared.Domain.Enum.TransactionStatus.موفق);
                    break;
                default:
                    break;
            }
            switch (portal)
            {
                case TransactionPortalSearch.زرین_پال:
                    result = result.Where(r => r.Portal == Shared.Domain.Enum.TransactionPortal.زرین_پال);
                    break;
                default:
                    break;
            }
            TransactionsForAdminPaging model = new();
            model.GetData(result, pageId, take, 2);
            model.Status = status;
            model.Transactions = new();
            model.TransactiionSuccessSum = 0;
            model.Filter = filter;
            model.OrderBy = orderby;
            model.PageTitle = title;
            model.UserId = userId;
            model.TransactionFor = transactionFor;
            model.Portal = portal;
            if (model.DataCount > 0)
                model.Transactions = result.Skip(model.Skip).Take(model.Take).
                    Select(t => new TransactionForAdminQueryModel
                    {
                        CretionDate = t.CreateDate.ToPersainDate(),
                        Id = t.Id,
                        OwnerId = t.OwnerId,
                        Portal = t.Portal,
                        Price = t.Price,
                        RefId = t.RefId,
                        Status = t.Status,
                        TransactionFor = t.TransactionFor,
                        UserId = t.UserId,
                        UserName = ""
                    }).ToList();

            model.Transactions.ForEach(x =>
            {
                var user = _userRepository.GetById(x.UserId);
                x.UserName = string.IsNullOrEmpty(user.FullName) ? user.Mobile : user.FullName;
            });
            return model;
        }

        public UserWalletForAdminPaging GetUserWalletsForAdmin(int pageId, int userId,int take,
            OrderingWalletSearch orderBy,
        WalletTypeSearch type, WalletWhySerch walletWhy)
        {
            var user = _userRepository.GetById(userId);
            var result = _walletRepository.GetAllByQuery(w=>w.UserId ==  userId && w.IsPay);
            switch (type)
            {
                case WalletTypeSearch.برداشت:
                    result = result.Where(r => r.Type == Shared.Domain.Enum.WalletType.برداشت);
                    break;
                case WalletTypeSearch.واریز:
                    result = result.Where(r => r.Type == Shared.Domain.Enum.WalletType.واریز);
                    break;
                default:
                    break;
            }
            switch (walletWhy)
            {
                case WalletWhySerch.توسط_ادمین:
                    result = result.Where(r => r.WalletWhy == Shared.Domain.Enum.WalletWhy.توسط_ادمین);
                    break;
                case WalletWhySerch.پرداخت_از_درگاه:
                    result = result.Where(r => r.WalletWhy == Shared.Domain.Enum.WalletWhy.پرداخت_از_درگاه);
                    break;
                case WalletWhySerch.خرید_از_سایت:
                    result = result.Where(r => r.WalletWhy == Shared.Domain.Enum.WalletWhy.خرید_از_سایت);
                    break;
                case WalletWhySerch.بازگشت_فاکتور:
                    result = result.Where(r => r.WalletWhy == Shared.Domain.Enum.WalletWhy.بازگشت_فاکتور);
                    break;
                case WalletWhySerch.کارت_هدیه:
                    break;
                default:
                    break;
            }
            switch (orderBy)
            {
                case OrderingWalletSearch.بر_اساس_تاریخ_از_آخر:
                    result = result.OrderByDescending(r => r.CreateDate);
                    break;
                case OrderingWalletSearch.بر_اساس_تاریخ_از_اول:
                    result = result.OrderBy(r => r.CreateDate);
                    break;
                case OrderingWalletSearch.بر_اساس_مبلغ_از_بالا_به_پایین:
                    result = result.OrderByDescending(r => r.Price);
                    break;
                case OrderingWalletSearch.بر_اساس_مبلغ_از_پایین_به_بالا:
                    result = result.OrderBy(r => r.Price);
                    break;
                default:
                    break;
            }
            UserWalletForAdminPaging model = new();
            model.GetData(result,pageId,take,2);
            model.UserId = userId;
            model.UserName  =string.IsNullOrEmpty(user.FullName) ? user.Mobile : user.FullName;
            model.WalletAmount = 0;
            model.OrderBy = orderBy;
            model.WalletWhy = walletWhy;
            model.Type = type;
            model.Wallets = new();
            if (model.DataCount > 0)
                model.Wallets = result.Skip(model.Skip).Take(model.Take)
                    .Select(w => new UserWalletAdminQueryModel
                    {
                        CreationDate = w.CreateDate.ToPersainDate(),
                        Description = w.Description,
                        Id = w.Id,
                        IsPay = w.IsPay,
                        Price = w.Price,
                        Type = w.Type,
                        WalletWhy = w.WalletWhy
                    }).ToList();

            model.WalletAmount = _walletRepository.GetWalletAmount(userId);
            return model;
        }
    }
}

using Query.Contract.Admin.User;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transactions.Domain;
using Users.Application.Contract.WalletApplication.Command;
using Users.Domain.UserAgg.Repository;
using Users.Domain.WalletAgg;

namespace Query.Services.Admin
{
    internal class AdminUserQuery : IAdminUserQuery
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;
        public AdminUserQuery(IUserRepository userRepository,IWalletRepository walletRepository,
            ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        public UsersForAdminPaging GetUsersForAdmin(int pageId, string filter, int take,
        UserOrderBySearch orderBy, UserStatusSearch status)
        {
            var result = _userRepository.GetAllQuery().OrderByDescending(u=>u.Id);
            if(!string.IsNullOrEmpty(filter) ) 
                result = result.Where(r=>
                r.Mobile.Contains(filter.ToLower().Trim()) ||
                r.Email.ToLower().Trim().Contains(filter.ToLower().Trim()) ||
                r.FullName.ToLower().Trim().Contains(filter.ToLower().Trim())
                ).OrderByDescending(u=>u.Id);

            switch (status)
            {
                case UserStatusSearch.حذف_شده_ها:
                    result = result.Where(r => r.IsDelete).OrderByDescending(u => u.Id);
                    break;
                case UserStatusSearch.کاربران_فعال:
                    result = result.Where(r => r.Active).OrderByDescending(u => u.Id);
                    break;
                case UserStatusSearch.کاربران_غیر_فعال:
                    result = result.Where(r => !r.Active).OrderByDescending(u => u.Id);
                    break;
                default:
                    break;
            }
            switch (orderBy)
            {
                case UserOrderBySearch.تاریخ_ثبت_از_اول_به_آخر:
                    result = result.OrderBy(u => u.Id);
                    break;
                default:
                    break;
            }
            UsersForAdminPaging model = new();
            model.GetData(result, pageId, take, 2);
            model.OrderBy = orderBy;
            model.Status = status;
            model.Filter = filter;
            model.Users = new();
            if (model.DataCount > 0)
                model.Users = result.Skip(model.Skip).Take(model.Take).
                    Select(u => new UserForAdminQueryModel
                    {
                        Active = u.Active,
                        WalletAmount = 0,
                        Creationdate = u.CreateDate.ToPersainDate(),
                        Delete = u.IsDelete,
                        Email = u.Email,
                        FullName = u.FullName,
                        Id = u.Id,
                        Mobile = u.Mobile,
                        OrderCount = 0,
                        OrderSum = 0,
                        TransactionSuccessCount = 0,
                        TransactionSuccessSum = 0
                    }).ToList();

            if (model.Users.Count() > 0)
                model.Users.ForEach(x =>
                {
                    x.WalletAmount = _walletRepository.GetWalletAmount(x.Id);
                    x.TransactionSuccessCount = _transactionRepository.GetAllByQuery(t => t.UserId == x.Id && t.Status == Shared.Domain.Enum.TransactionStatus.موفق).Count();
                    x.TransactionSuccessSum = _transactionRepository.GetAllByQuery(t => t.UserId == x.Id && t.Status == Shared.Domain.Enum.TransactionStatus.موفق).Sum(t=>t.Price);
                });

            return model;   
        }
    }
}

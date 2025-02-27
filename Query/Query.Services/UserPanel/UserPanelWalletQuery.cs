using Query.Contract.UserPanel.Wallet;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transactions.Domain;
using Users.Domain.UserAgg.Repository;
using Users.Domain.WalletAgg;

namespace Query.Services.UserPanel
{
    internal class UserPanelWalletQuery : IUserPanelWalletQuery
    {
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;

        public UserPanelWalletQuery(IUserRepository userRepository, 
            ITransactionRepository transactionRepository, IWalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
        }

        public TransactionUserPanelPaging GetTransactionsForUserPanel(int userId,
            int pageId, string filter)
        {
            IQueryable<Transaction> result = _transactionRepository.GetAllByQuery(t => t.UserId == userId).OrderByDescending(o => o.Id);
            if(!string.IsNullOrEmpty(filter))
                result  = result.Where(r=>r.RefId.Contains(filter)).OrderByDescending(r=>r.Id);

            TransactionUserPanelPaging model = new();
            model.GetData(result, pageId, 15, 2);
            model.Filter = filter;
            model.FullName = "";
            model.WalletAmount = 0;
            model.UserId = userId;
            model.Transaction = new();
            if (result.Count() > 0)
                model.Transaction = result.Skip(model.Skip).Take(model.Take).Select(t => new
                TransactionUserPanelQueryModel()
                {
                    CretionDate = t.CreateDate.ToPersainDate(),
                    Id = t.Id,
                    OwnerId = t.OwnerId,
                    Portal =t.Portal,
                    Price = t.Price,
                    RefId = t.RefId,
                    Status = t.Status,
                    TransactionFor = t.TransactionFor
                }).ToList();

            model.WalletAmount = _walletRepository.GetWalletAmount(model.UserId);
            var user = _userRepository.GetById(userId);
            model.FullName = string.IsNullOrEmpty(user.FullName) ? user.Mobile : user.FullName;
            return model;
        }

        public int GetUserWalletAmount(int userId)=>
            _walletRepository.GetWalletAmount(userId);

        public WalletUserPanelPaging GetWalletsForUserPanel(int userId, int pageId, string filter)
        {
            IQueryable<Wallet> result = _walletRepository.GetAllByQuery(w => w.UserId == userId && w.IsPay).OrderByDescending(w => w.Id);
            if(!string.IsNullOrEmpty(filter))
                result = result.Where(r=>r.Description.Contains(filter)).OrderByDescending(w=>w.Id);
            
            WalletUserPanelPaging model = new WalletUserPanelPaging();
            model.GetData(result, pageId, 15, 2);
            model.Filter = filter;
            model.FullName = "";
            model.UserId = userId;
            model.WalletAmount = 0;
            model.Wallets = new();
            if (result.Count() > 0)
                model.Wallets = result.Skip(model.Skip).Take(model.Take)
                    .Select(w => new WalletUserPanelQueryModel
                    {
                        CreationDate = w.CreateDate.ToPersainDate(),
                        Description = w.Description,    
                        Id = w.Id,
                        Price = w.Price,
                        Type = w.Type
                    }).ToList();

            model.WalletAmount = _walletRepository.GetWalletAmount(model.UserId);
            var user = _userRepository.GetById(userId);
            model.FullName = string.IsNullOrEmpty(user.FullName) ? user.Mobile : user.FullName;
            return model;
        }
    }
}

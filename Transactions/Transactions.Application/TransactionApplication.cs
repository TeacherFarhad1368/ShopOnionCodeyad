using Shared.Application;
using Shared.Domain.Enum;
using Transactions.Application.Contract;
using Transactions.Domain;

namespace Transactions.Application
{
    internal class TransactionApplication : ITransactionApplication
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionApplication(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> AddTransactionWalletId(long transactionId, int walletId)
        {
            Transaction transaction = await _transactionRepository.GetByIdAsync(transactionId);
            transaction.AddWalletId(walletId);
            return await _transactionRepository.SaveAsync();
        }

        public async Task<OperationResultWithKeylong> CreateAsync(CreateTransaction command)
        {
            if (command.Price < 1000)
                return new OperationResultWithKeylong(false, ValidationMessages.PaymentPriceError, nameof(command.Price));
            if(await _transactionRepository.ExistByAsync(t=>t.Authority == command.Authority || string.IsNullOrEmpty(command.Authority)))
                return new OperationResultWithKeylong(false,"عملیات نا موفق !!!", nameof(command.Authority));
            Transaction transaction = new(command.UserId, command.Price, command.Portal, command.TransactionFor, command.OwnerId,command.Authority);
            var res = await _transactionRepository.CreateAsyncReturnKey(transaction);
            if (res > 0)
                return new(true,"","",res);
            return new OperationResultWithKeylong(false, ValidationMessages.SystemErrorMessage, nameof(command.Price));

        }

        public async Task<TransactionQueryModel> GetForCheckPaymentAsync(string authority)
        {
            var tranaction = await _transactionRepository.GetByAuthorityAsync(authority);
            if (tranaction == null) return null;
            return new TransactionQueryModel(tranaction.Id,tranaction.UserId,tranaction.Price,tranaction.RefId,
                tranaction.Portal,tranaction.Status,tranaction.TransactionFor,tranaction.OwnerId);
        }

        public async Task<TransactionViewModel> GetTransactionForCheckPayment(long id)
        {
            var t = await _transactionRepository.GetByIdAsync(id);
            return new TransactionViewModel(t.Id, t.UserId, t.Price, t.RefId, t.Portal, t.Status, t.TransactionFor, t.OwnerId);
        }

        public async Task<bool> PaymentAsync(TransactionStatus status, long id, string refId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            transaction.Payment(status, refId);
            return await _transactionRepository.SaveAsync();
        }
    }
}

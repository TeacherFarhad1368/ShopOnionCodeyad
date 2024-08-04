using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Contract.WalletApplication.Command;
using Users.Domain.WalletAgg;

namespace Users.Application.Services;

internal class WalletApplication : IWalletApplication
{
    private readonly IWalletRepository _walletRepository;
    public WalletApplication(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }
    public async Task<OperationResult> DepositByAdminAsync(CreateWallet command)
    {
        Wallet wallet = Wallet.DepositByAdmin(command.UserId, command.Price, command.Description);
        if (await _walletRepository.CreateAsync(wallet))
            return new(true);

        return new(false, ValidationMessages.SystemErrorMessage);
    }

    public async Task<OperationResultWithKey> DepositByUserAsync(CreateWalletWithWhy command)
    {
        return await _walletRepository.DepositByUserAsync(command);

    }

    public async Task<WalletForCheckPayemntQueryModel> GetWalletForCheckPaymentAsync(int id)
    {
        var wallet = await _walletRepository.GetByIdAsync(id);
        return new(wallet.Id, wallet.Type, wallet.IsPay, wallet.Description);
    }

    public async Task<bool> SuccessPaymentAsync(int id)
    {
        var wallet = await _walletRepository.GetByIdAsync(id);
        wallet.PaymentSuccess();
        return await _walletRepository.SaveAsync();
    }

    public async Task<OperationResult> WithdrawalAsync(CreateWalletWithWhy command)
    {
        var wallet = Wallet.Withdrawall(command.UserId, command.Price, command.Description, command.WalletWhy);
        if (await _walletRepository.CreateAsync(wallet))
            return new(true);

        return new(false, ValidationMessages.SystemErrorMessage);
    }
}

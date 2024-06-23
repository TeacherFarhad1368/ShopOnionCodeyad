using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.UserPanel.Wallet;

public interface IUserPanelWalletQuery
{
    TransactionUserPanelPaging GetTransactionsForUserPanel(int userId, int pageId, string filter);
    WalletUserPanelPaging GetWalletsForUserPanel(int userId, int pageId, string filter);
}

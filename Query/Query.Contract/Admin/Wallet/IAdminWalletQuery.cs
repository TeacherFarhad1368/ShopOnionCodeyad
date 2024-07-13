namespace Query.Contract.Admin.Wallet;

public interface IAdminWalletQuery
{
    TransactionsForAdminPaging GetTransactionsForAdmin(int pageId, int userId,
        string filter, int take,OrderingWalletSearch orderby
        ,TransactionForSearch transactionFor,TransactionStatusSearch status,TransactionPortalSearch portal);
    UserWalletForAdminPaging GetUserWalletsForAdmin(int pageId, int userId, int take,
        OrderingWalletSearch orderBy
        ,WalletTypeSearch type, WalletWhySerch walletWhy);

}

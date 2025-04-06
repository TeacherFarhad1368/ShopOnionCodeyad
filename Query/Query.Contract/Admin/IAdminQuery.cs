namespace Query.Contract.Admin;
public interface IAdminQuery
{
    AdminDataQueryModel GetAdminData();
    TransactionChartQueryModel GetTransactionChartData(string Year);
}

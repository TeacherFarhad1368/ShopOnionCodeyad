namespace Query.Contract.Admin;

public class AdminDataQueryModel
{
    public int UserCount { get; set; }
    public int UserCountDay { get; set; }
    public int UserCountWeek { get; set; }
    public int UserCountMounth { get; set; }
    public int BlogCount { get; set; }
    public int BlogCountComment { get; set; }
    public int BlogCountVisit { get; set; }
    public int ProductCount { get; set; }
    public int ProductCountSell { get; set; }
    public int ProductCountVisit { get; set; }
    public int ProductCountComment { get; set; }
    public int OrderCount { get; set; }
    public int OrderSellerCount { get; set; }
    public int OrderItemCount { get; set; }
    public int OrderPostCount { get; set; }
    public int TransactionCount { get; set; }
    public int TransactionSum { get; set; }
}

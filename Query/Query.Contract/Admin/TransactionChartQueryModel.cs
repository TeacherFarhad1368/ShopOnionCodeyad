namespace Query.Contract.Admin;

public class TransactionChartQueryModel
{
    public string Year { get; set; }
    public List<string> Years { get; set; }
    public string[] Mounth { get; set; } = new string[]
    {
        "فروردین",
        "اردیبهشت",
        "خرداد",
        "تیر",
        "مرداد",
        "شهریور",
        "مهر",
        "آبان",
        "آذر",
        "دی",
        "بهمن",
        "اسفند"
    };
    public List<int> Prices { get; set; }
}
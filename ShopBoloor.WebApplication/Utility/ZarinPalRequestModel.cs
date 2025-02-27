namespace ShopBoloor.WebApplication.Utility;
public class ZarinPalRequestModel
{
    public string merchant_id { get; set; }
    public int amount { get; set; }
    public string currency { get; set; }
    public string description { get; set; }
    public string callback_url { get; set; }
    public string mobile { get; set; }
}
public class ZarinPalDataResponseModel
{
    public int code { get; set; }
    public string message { get; set; }
    public string authority { get; set; }
    public string fee_type { get; set; }
    public int fee { get; set; }
}
public class ZarinPalResponseModel
{
    public ZarinPalDataResponseModel data { get; set; }
    public string[] errors { get; set; }
}
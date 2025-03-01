namespace ShopBoloor.WebApplication.Utility;

public class ZarinPalPaymentRequestModel
{
    public string merchant_id { get; set; }
    public int amount { get; set; }
    public string authority { get; set; }
}
public class ZarinPalPaymentResponseModel
{
    public int code { get; set; }
    public int ref_id { get; set; }
    public string message { get; set; }
}
public class ZarinpalPaymentResponseModel
{
    public ZarinPalPaymentResponseModel data { get; set; }
    public string[] errors { get; set; }
}
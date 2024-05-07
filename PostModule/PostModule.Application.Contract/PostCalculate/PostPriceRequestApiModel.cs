namespace PostModule.Application.Contract.PostCalculate;

public class PostPriceRequestApiModel
{
    public string ApiCode { get; set; }
    public int SourceCityId { get; set; }
    public int DestinationCityId { get; set; }
    public int Weight { get; set; }
}
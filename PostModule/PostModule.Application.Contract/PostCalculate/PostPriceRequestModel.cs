namespace PostModule.Application.Contract.PostCalculate;

public class PostPriceRequestModel
{
    public int SourceCityId { get; set; }
    public int DestinationCityId { get; set; }
    public int Weight { get; set; }
}
namespace WebApplication.Test.Models
{
    public class City
    {
        public int CityCode { get; set; }
        public string Name { get; set; }
    }
    public class State
    {
        public string Name { get; set; }
        public List<City> Cities { get; set; }
    }
    public class PostPriceRequestModel
    {
        public string ApiCode { get; set; }
        public int SourceCityId { get; set; }
        public int DestinationCityId { get; set; }
        public int Weight { get; set; }
    }
    public class PostPriceResponseModel
    {  

        public string Title { get; set; }
        public string Status { get; set; }
        public int Price { get; set; }
    }
    public class PostPriceResponseApiModel
    {

        public List<PostPriceResponseModel> Prices { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}

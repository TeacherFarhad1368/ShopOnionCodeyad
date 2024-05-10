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
}

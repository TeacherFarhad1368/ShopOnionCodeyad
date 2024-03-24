namespace PostModule.Application.Contract.StateQuery;

public class StateQueryModel
{
    public string Name { get; set; }
    public List<CityQueryModel> Cities { get; set; }
}

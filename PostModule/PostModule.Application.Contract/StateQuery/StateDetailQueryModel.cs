namespace PostModule.Application.Contract.StateQuery;

public class StateDetailQueryModel
{
    public int Id { get; set; }
    public string CloseStates { get; set; }
	public List<StateForAddStateClosesQueryModel> States { get; set; }
	public string Name { get; set; }
	public List<CityAdminQueryModel> Cities { get; set; }

}
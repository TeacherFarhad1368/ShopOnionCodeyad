namespace PostModule.Application.Contract.UserPostApplication.Command;

public class UserPostPanelModel
{
    public UserPostPanelModel(string? apiDescription, int count, string apiCode)
    {
        ApiDescription = apiDescription;
        Count = count;
        ApiCode = apiCode;
    }

    public string? ApiDescription { get; private set; }
    public int Count { get; private set; }
    public string ApiCode { get; private set; }
}
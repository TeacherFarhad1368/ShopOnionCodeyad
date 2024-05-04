namespace PostModule.Application.Contract.UserPostApplication.Command;

public class EditPackage : CreatePackage
{
    public int Id { get; set; }
    public string ImageName { get; set; }
}
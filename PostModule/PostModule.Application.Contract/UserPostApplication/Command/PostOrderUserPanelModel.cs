namespace PostModule.Application.Contract.UserPostApplication.Command;

public class PostOrderUserPanelModel
{
    public PostOrderUserPanelModel(int id, int packageId, string packageTitle,
        int price, string imageName, string imageAlt, int count, string description)
    {
        Id = id;
        PackageId = packageId;
        PackageTitle = packageTitle;
        Price = price;
        ImageName = imageName;
        ImageAlt = imageAlt;
        Count = count;
        Description = description;
    }

    public int Id { get; private set; }
    public int PackageId { get; private set; }
    public string PackageTitle { get; private set; }
    public int Price { get; private set; }
    public string ImageName { get; private set; }
    public string ImageAlt { get; private set; }
    public string Description { get; private set; }
    public int Count { get; private set; }
}
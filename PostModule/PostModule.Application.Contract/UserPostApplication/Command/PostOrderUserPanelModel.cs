namespace PostModule.Application.Contract.UserPostApplication.Command;

public class PostOrderUserPanelModel
{
    public PostOrderUserPanelModel(int id, int packageId, string packageTitle,
        int price, string imageName, string imageAlt, int count)
    {
        Id = id;
        PackageId = packageId;
        PackageTitle = packageTitle;
        Price = price;
        ImageName = imageName;
        ImageAlt = imageAlt;
        Count = count;
    }

    public int Id { get; private set; }
    public int PackageId { get; private set; }
    public string PackageTitle { get; private set; }
    public int Price { get; private set; }
    public string ImageName { get; private set; }
    public string ImageAlt { get; private set; }
    public int Count { get; private set; }
}
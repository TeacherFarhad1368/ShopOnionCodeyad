namespace PostModule.Application.Contract.PostCalculate;

public class PostPriceResponseModel
{
    public PostPriceResponseModel(string title, string status, int price,int id)
    {
        Title = title;
        Status = status; 
        Price = price;
        PostId = id;
    }

    public int PostId { get; set; } 
    public string Title { get; private set; }
    public string Status { get; private set; }
    public int Price { get; private set; }
}

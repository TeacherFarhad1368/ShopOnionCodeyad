using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Contract.PostCalculate;

public interface IPostCalculateApplication
{
    Task<List<PostPriceResponseModel>> CalculatePost(PostPriceRequestModel command);
    Task<PostPriceResponseApiModel> CalculatePostForApi(PostPriceRequestApiModel command);
}
public class PostPriceResponseApiModel
{
    public PostPriceResponseApiModel(List<PostPriceResponseModel> prices, string message, bool success)
    {
        Prices = prices;
        Message = message;
        Success = success;
    }

    public List<PostPriceResponseModel> Prices { get; private set; }
    public string Message { get; private set; }
    public bool Success { get; private set; }
}
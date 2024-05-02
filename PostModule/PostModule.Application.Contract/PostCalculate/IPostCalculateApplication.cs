using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Contract.PostCalculate;

public interface IPostCalculateApplication
{
    Task<List<PostPriceResponseModel>> CalculatePost(PostPriceRequestModel command);
}

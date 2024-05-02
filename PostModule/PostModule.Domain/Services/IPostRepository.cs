using Shared.Domain;
using PostModule.Application.Contract.PostApplication;
using PostModule.Domain.PostEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostModule.Application.Contract.PostCalculate;

namespace PostModule.Domain.Services
{
    public interface IPostRepository : IRepository<int, Post>
    {
        Task<List<PostPriceResponseModel>> CalculatePostAsync(PostPriceRequestModel command);
        EditPost GetForEdit(int id);
    }
}

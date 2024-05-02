using PostModule.Application.Contract.PostCalculate;
using PostModule.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Services
{
    internal class PostCalculateApplication : IPostCalculateApplication
    {
        private readonly IPostRepository _postRepository;

        public PostCalculateApplication(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<PostPriceResponseModel>> CalculatePost(PostPriceRequestModel command)
        {
            return await _postRepository.CalculatePostAsync(command);
        }
    }
}

using PostModule.Application.Contract.PostCalculate;
using PostModule.Domain.Services;
using PostModule.Domain.UserPostAgg;
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
        private readonly IUserPostRepository _userPostRepository;
        public PostCalculateApplication(IPostRepository postRepository, IUserPostRepository userPostRepository)
        {
            _postRepository = postRepository;
            _userPostRepository = userPostRepository;
        }

        public async Task<List<PostPriceResponseModel>> CalculatePost(PostPriceRequestModel command)
        {
            return await _postRepository.CalculatePostAsync(command);
        }

        public async Task<PostPriceResponseApiModel> CalculatePostForApi(PostPriceRequestApiModel command)
        {
            UserPost userPost = await _userPostRepository.GetByApiCode(command.ApiCode);
            if (userPost == null)
                return new PostPriceResponseApiModel(new List<PostPriceResponseModel>(), "کاربری یافت نشد", false);
            if (userPost.Count < 1)
                return new PostPriceResponseApiModel(new List<PostPriceResponseModel>(), "درخواست های Api شما تمام شده لطفا به سایت مراجعه کرده و بسته جدیدی تهیه کنید .", false);

            var prices = await CalculatePost(new PostPriceRequestModel()
            {
                DestinationCityId = command.DestinationCityId,
                SourceCityId = command.SourceCityId,
                Weight = command.Weight
            });
            if(prices.Count() < 1)
                return new PostPriceResponseApiModel(new List<PostPriceResponseModel>(),
                    "خطای سیستم . با سایت تماس بگیرید .", false);
            userPost.UseApi();
            _userPostRepository.Save();


            return new(prices, "عملیات موفق", true);
        }
    }
}

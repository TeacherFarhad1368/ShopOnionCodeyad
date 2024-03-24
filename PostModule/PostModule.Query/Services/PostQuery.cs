using PostModule.Application.Contract.PostQuery;
using PostModule.Domain.Services;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Query.Services
{
    internal class PostQuery : IPostQuery
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostPriceRepository _postPriceRepository;

        public PostQuery(IPostRepository postRepository, IPostPriceRepository postPriceRepository)
        {
            _postRepository = postRepository;
            _postPriceRepository = postPriceRepository;
        }

        public List<PostAdminQueryModel> GetAllPostsForAdmin()
        {
            return _postRepository.GetAllQuery()
                .Select(p => new PostAdminQueryModel
                {
                    Active = p.Active,
                    CreationDate = p.CreateDate.ToPersainDate(),
                    Id = p.Id,
                    InsideCity = p.InsideCity,
                    OutsideCity = p.OutSideCity,
                    Title = p.Title
                }).ToList();
        }

        public PostAdminDetailQueryModel GetPostDetails(int id)
        {
            var post = _postRepository.GetById(id);
            var prices = _postPriceRepository.GetAllByQuery(p => p.PostId == post.Id);
            PostAdminDetailQueryModel model = new()
            {
                Active = post.Active,
                CityPricePlus = post.CityPricePlus,
                CreationDate = post.CreateDate.ToPersainDate(),
                Description = post.Description,
                Id = post.Id,
                InsideCity = post.InsideCity,
                InsideStatePricePlus = post.InsideStatePricePlus,
                OutsideCity = post.OutSideCity,
                PostPrices = new(),
                StateCenterPricePlus = post.StateCenterPricePlus,
                StateClosePricePlus = post.StateClosePricePlus,
                StateNonClosePricePlus = post.StateNonClosePricePlus,
                Status = post.Status,
                TehranPricePlus = post.TehranPricePlus,
                Title = post.Title
            };
            model.PostPrices = prices.Select(p => new PostPriceAdminQueryModel
            {
                CityPrice = p.CityPrice,
                End = p.End,
                Id = p.Id,
                InsideStatePrice = p.InsideStatePrice,
                Start = p.Start,
                StateCenterPrice = p.StateCenterPrice,
                StateClosePrice = p.StateClosePrice,
                StateNonClosePrice = p.StateNonClosePrice,
                TehranPrice = p.TehranPrice
            }).ToList();
            return model;
        }
    }
}

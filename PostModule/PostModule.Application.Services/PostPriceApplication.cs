using Shared.Application;
using PostModule.Application.Contract.PostPriceApplication;
using PostModule.Domain.PostEntity;
using PostModule.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Services
{
    internal class PostPriceApplication : IPostPriceApplication
    {
        private readonly IPostPriceRepository _postPriceRepository;
        public PostPriceApplication(IPostPriceRepository postPriceRepository)
        {
            _postPriceRepository = postPriceRepository;
        }

        public OperationResult Create(CreatePostPrice command)
        {
            PostPrice postPrice = new(command.PostId, command.Start, command.End, command.TehranPrice,
                command.StateCenterPrice, command.CityPrice, command.InsideStatePrice,
                command.StateClosePrice, command.StateNonClosePrice);
            if (_postPriceRepository.Create(postPrice))
                return new(true);

            return new(false, ValidationMessages.SystemErrorMessage, "Start");
        }

        public OperationResult Edit(EditPostPrice command)
        {
            var postPrice = _postPriceRepository.GetById(command.Id);
            postPrice.Edit(command.Start, command.End, command.TehranPrice,
                command.StateCenterPrice, command.CityPrice, command.InsideStatePrice,
                command.StateClosePrice, command.StateNonClosePrice);
            if (_postPriceRepository.Save())
                return new(true);

            return new(false, ValidationMessages.SystemErrorMessage, "Start");
        }

        public List<PostPriceModel> GetAllForPost(int postId)
        {
            return _postPriceRepository.GetAllForPost(postId);
        }

        public EditPostPrice GetForEdit(int id)
        {
            return _postPriceRepository.GetForEdit(id);
        }
    }
}

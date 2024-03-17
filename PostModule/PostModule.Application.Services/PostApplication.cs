using Shared.Application;
using PostModule.Application.Contract.PostApplication;
using PostModule.Domain.PostEntity;
using PostModule.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Services
{
    internal class PostApplication : IPostApplication
    {
        private readonly IPostRepository _postRepository;
        public PostApplication(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public bool ActivationChange(int id)
        {
            var post = _postRepository.GetById(id);
            post.ActivationChange();
            return _postRepository.Save();
        }

        public OperationResult Create(CreatePost command)
        {
            if (_postRepository.ExistBy(p => p.Title == command.Title))
                return new OperationResult(false, ValidationMessages.DuplicatedMessage, "Title");
            Post post = new(command.Title, command.Status, command.TehranPricePlus, command.StateCenterPricePlus,
               command.CityPricePlus, command.InsideStatePricePlus, command.StateClosePricePlus,
               command.StateNonClosePricePlus,command.Description);
            if (_postRepository.Create(post))
                return new(true);

            return new OperationResult(false, ValidationMessages.SystemErrorMessage, "Title");
        }

        public OperationResult Edit(EditPost command)
        {
            if (_postRepository.ExistBy(p => p.Title == command.Title && p.Id != command.Id))
                return new OperationResult(false, ValidationMessages.DuplicatedMessage, "Title");
            var post = _postRepository.GetById(command.Id);
             post.Edit(command.Title, command.Status, command.TehranPricePlus, command.StateCenterPricePlus,
               command.CityPricePlus, command.InsideStatePricePlus, command.StateClosePricePlus,
               command.StateNonClosePricePlus,command.Description);
            if (_postRepository.Save())
                return new(true);

            return new OperationResult(false, ValidationMessages.SystemErrorMessage, "Title");
        }

        public List<PostModel> GetAll()
        {
            return _postRepository.GetAllPosts();
        }

        public EditPost GetForEdit(int id)
        {
            return _postRepository.GetForEdit(id);
        }

        public bool InsideCityChange(int id)
        {
            var post = _postRepository.GetById(id);
            post.InsideCityChange();
            return _postRepository.Save();
        }

        public bool OutSideCityChange(int id)
        {
            var post = _postRepository.GetById(id);
            post.OutSideCityChange();
            return _postRepository.Save();
        }
    }
}

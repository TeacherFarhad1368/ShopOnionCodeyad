using Blogs.Application.Contract.BlogApplication.Command;
using Blogs.Domain.BlogAgg;
using Blogs.Domain.BlogCategoryAgg;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Shared;
using Shared.Application;
using Shared.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services
{
    internal class BlogApplication : IBlogApplication
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IFileService _fileService;
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        public BlogApplication(IBlogRepository blogRepository,IFileService fileService,
            IBlogCategoryRepository blogCategoryRepository)
        {
            _blogRepository = blogRepository;
            _fileService = fileService;
            _blogCategoryRepository = blogCategoryRepository;
        }

        public bool ActivationChange(int id)
        {
            var blog = _blogRepository.GetById(id);
            blog.ActivationChange();
            return _blogRepository.Save();
        }

        public OperationResult Create(CreateBlog command)
        {
            command.Slug = command.Slug.GenerateSlug();
            if (_blogRepository.ExistBy(b => b.Title.Trim() == command.Title.Trim()))
                return new(false, ValidationMessages.DuplicatedMessage, "Title");

            var slug = SlugUtility.GenerateSlug(command.Slug);
            if (_blogRepository.ExistBy(b => b.Slug.Trim() == slug))
                return new(false, ValidationMessages.DuplicatedMessage, "Slug");

            if(command.ImageFile == null || !command.ImageFile.IsImage())
                return new(false, ValidationMessages.ImageErrorMessage, "ImageFile");

            if(command.CategoryId < 1 || 
                _blogCategoryRepository.ExistBy(c=>c.Id == command.CategoryId && c.Parent == 0) == false)
                return new(false, ValidationMessages.ParentCategoryMessage, nameof(command.CategoryId));

            if(command.SubCategoryId > 0 && _blogCategoryRepository.ExistBy(c=>c.Id == command.SubCategoryId && c.Parent == command.CategoryId) == false)
                return new(false, ValidationMessages.ChildCategoryMessage, nameof(command.SubCategoryId));

            string imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.BlogImageFolder);
            if(imageName == "")
                return new(false, ValidationMessages.SystemErrorMessage, "Title");

            _fileService.ResizeImage(imageName, FileDirectories.BlogImageFolder, 400);
            _fileService.ResizeImage(imageName, FileDirectories.BlogImageFolder, 100);

            Blog blog = new(command.Title, slug, command.ShortDescription,command.Text, imageName, command.ImageAlt,
                command.CategoryId, command.SubCategoryId, command.UserId, command.Writer);

            if (_blogRepository.Create(blog)) return new(true);
            _fileService.DeleteImage($"{FileDirectories.BlogImageDirectory}{imageName}");
            _fileService.DeleteImage($"{FileDirectories.BlogImageDirectory400}{imageName}");
            _fileService.DeleteImage($"{FileDirectories.BlogImageDirectory100}{imageName}");
            return new(false, ValidationMessages.SystemErrorMessage, "Title");
        }

        public OperationResult Edit(EditBlog command)
        {
            command.Slug = command.Slug.GenerateSlug();
            var blog = _blogRepository.GetById(command.Id);
            if (_blogRepository.ExistBy(b => b.Title.Trim() == command.Title.Trim() && b.Id != command.Id))
                return new(false, ValidationMessages.DuplicatedMessage, "Title");

            var slug = SlugUtility.GenerateSlug(command.Slug);
            if (_blogRepository.ExistBy(b => b.Slug.Trim() == slug && b.Id != command.Id))
                return new(false, ValidationMessages.DuplicatedMessage, "Slug");

            if (command.ImageFile != null && !command.ImageFile.IsImage())
                return new(false, ValidationMessages.ImageErrorMessage, "ImageFile");

            if (command.CategoryId < 1 || _blogCategoryRepository.ExistBy(c => c.Id == command.CategoryId && c.Parent == 0) == false)
                return new(false, ValidationMessages.ParentCategoryMessage, nameof(command.CategoryId));

            if (command.SubCategoryId > 0 && _blogCategoryRepository.ExistBy(c => c.Id == command.SubCategoryId && c.Parent == command.CategoryId) == false)
                return new(false, ValidationMessages.ChildCategoryMessage, nameof(command.SubCategoryId));

            string imageName = command.ImageName;
            string oldImageName = command.ImageName;
            if (command.ImageFile != null)
            {
                imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.BlogImageFolder);
                if (imageName == "")
                    return new(false, ValidationMessages.SystemErrorMessage, "Title");
                _fileService.ResizeImage(imageName, FileDirectories.BlogImageFolder, 400);
                _fileService.ResizeImage(imageName, FileDirectories.BlogImageFolder, 100);
            }
            blog.Edit(command.Title, slug, command.ShortDescription,command.Text, imageName, command.ImageAlt,
                command.CategoryId, command.SubCategoryId, command.Writer);
            if (_blogRepository.Save())
            {
                if (command.ImageFile != null)
                {
                    _fileService.DeleteImage($"{FileDirectories.BlogImageDirectory}{oldImageName}");
                    _fileService.DeleteImage($"{FileDirectories.BlogImageDirectory400}{oldImageName}");
                    _fileService.DeleteImage($"{FileDirectories.BlogImageDirectory100}{oldImageName}");
                }
                return new(true);
            }
            if (command.ImageFile != null)
            {
                _fileService.DeleteImage($"{FileDirectories.BlogImageDirectory}{imageName}");
                _fileService.DeleteImage($"{FileDirectories.BlogImageDirectory400}{imageName}");
                _fileService.DeleteImage($"{FileDirectories.BlogImageDirectory100}{imageName}");
            }
                return new(false, ValidationMessages.SystemErrorMessage, "Title");
        }

        public EditBlog GetForEdit(int id)
        {
            return _blogRepository.GetForEdit(id);
        }

        public bool VisitBlog(int id)
        {
            var blog = _blogRepository.GetById(id);
            blog.VisitPlus();
            return _blogRepository.Save();
        }
    }
}

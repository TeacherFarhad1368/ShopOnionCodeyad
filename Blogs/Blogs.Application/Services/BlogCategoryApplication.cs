using Blogs.Application.Contract.BlogCategoryApplication.Command;
using Blogs.Domain.BlogAgg;
using Blogs.Domain.BlogCategoryAgg;
using Shared;
using Shared.Application;
using Shared.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services
{
    internal class BlogCategoryApplication : IBlogCategoryApplication
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IFileService _fileService;
        public BlogCategoryApplication(IBlogCategoryRepository blogCategoryRepository, IFileService fileService)
        {
            _blogCategoryRepository = blogCategoryRepository;
            _fileService = fileService;
        }

        public bool ActivationChange(int id)
        {
            var category = _blogCategoryRepository.GetById(id);
            category.ActivationChange();
            return _blogCategoryRepository.Save();
        }

        public OperationResult Create(CreateBlogCategory command)
        {
            command.Slug = command.Slug.GenerateSlug();

            if (_blogCategoryRepository.ExistBy(b => b.Title.Trim() == command.Title.Trim()))
                return new(false, ValidationMessages.DuplicatedMessage, "Title");

            var slug = SlugUtility.GenerateSlug(command.Slug);
            if (_blogCategoryRepository.ExistBy(b => b.Slug.Trim() == slug))
                return new(false, ValidationMessages.DuplicatedMessage, "Slug");

            if (command.ImageFile == null || !command.ImageFile.IsImage())
                return new(false, ValidationMessages.ImageErrorMessage, "ImageFile");

            string imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.BlogCategoryImageFolder);
            if (imageName == "")
                return new(false, ValidationMessages.SystemErrorMessage, "Title");
            _fileService.ResizeImage(imageName, FileDirectories.BlogCategoryImageFolder, 400);
            _fileService.ResizeImage(imageName, FileDirectories.BlogCategoryImageFolder, 100);

            BlogCategory category = new(command.Title, slug, imageName, command.ImageAlt, command.Parent);
            if (_blogCategoryRepository.Create(category)) return new(true);
            _fileService.DeleteImage($"{FileDirectories.BlogCategoryImageDirectory}{imageName}");
            _fileService.DeleteImage($"{FileDirectories.BlogCategoryImageDirectory400}{imageName}");
            _fileService.DeleteImage($"{FileDirectories.BlogCategoryImageDirectory100}{imageName}");
            return new(false, ValidationMessages.SystemErrorMessage, "Title");
        }

        public OperationResult Edit(EditBlogCategory command)
        {
            command.Slug = command.Slug.GenerateSlug();
            var category = _blogCategoryRepository.GetById(command.Id);
            if (_blogCategoryRepository.ExistBy(b => b.Title.Trim() == command.Title.Trim() && b.Id != command.Id))
                return new(false, ValidationMessages.DuplicatedMessage, "Title");

            var slug = SlugUtility.GenerateSlug(command.Slug);
            if (_blogCategoryRepository.ExistBy(b => b.Slug.Trim() == slug && b.Id != command.Id))
                return new(false, ValidationMessages.DuplicatedMessage, "Slug");

            if (command.ImageFile != null && !command.ImageFile.IsImage())
                return new(false, ValidationMessages.ImageErrorMessage, "ImageFile");

            string imageName = command.ImageName;
            string oldImageName = command.ImageName;
            if (command.ImageFile != null)
            {
                imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.BlogCategoryImageFolder);
                if (imageName == "")
                    return new(false, ValidationMessages.SystemErrorMessage, "Title");
                _fileService.ResizeImage(imageName, FileDirectories.BlogCategoryImageFolder, 400);
                _fileService.ResizeImage(imageName, FileDirectories.BlogCategoryImageFolder, 100);
            }

            category.Edit(command.Title, slug, imageName, command.ImageAlt);

            if (_blogCategoryRepository.Save())
            {
                if (command.ImageFile != null)
                {
                    _fileService.DeleteImage($"{FileDirectories.BlogCategoryImageDirectory}{oldImageName}");
                    _fileService.DeleteImage($"{FileDirectories.BlogCategoryImageDirectory400}{oldImageName}");
                    _fileService.DeleteImage($"{FileDirectories.BlogCategoryImageDirectory100}{oldImageName}");
                }
                return new(true);
            }
            if (command.ImageFile != null)
            {
                _fileService.DeleteImage($"{FileDirectories.BlogCategoryImageDirectory}{imageName}");
                _fileService.DeleteImage($"{FileDirectories.BlogCategoryImageDirectory400}{imageName}");
                _fileService.DeleteImage($"{FileDirectories.BlogCategoryImageDirectory100}{imageName}");
            }
            return new(false, ValidationMessages.SystemErrorMessage, "Title");
        }

        public EditBlogCategory GetForEdit(int id)
        {
            return _blogCategoryRepository.GetForEdit(id);
        }
    }
}

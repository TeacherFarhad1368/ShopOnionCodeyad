using Shared;
using Shared.Application;
using Shared.Application.Services;
using Shop.Application.Contract.ProductCategoryApplication.Command;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
	internal class ProductCategoryApplication : IProductCategoryApplication
	{
		private readonly IProductCategoryRepository _productCategoryRepository;
		private readonly IFileService _fileService;

		public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileService fileService)
		{
			_productCategoryRepository = productCategoryRepository;
			_fileService = fileService;
		}

		public async Task<bool> ActivationChange(int id)
		{
			var category = await _productCategoryRepository.GetByIdAsync(id);
			category.ActivationChange();
			return await _productCategoryRepository.SaveAsync();
		}

		public async Task<OperationResult> CreateAsync(CreateProductCategory command)
		{
			if (await _productCategoryRepository.ExistByAsync(p => p.Title.Trim().ToLower() == command.Title.Trim().ToLower()))
				return new OperationResult(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));
			var slug = SlugUtility.GenerateSlug(command.Slug);
			if (await _productCategoryRepository.ExistByAsync(p => p.Slug == slug))
				return new OperationResult(false, ValidationMessages.DuplicatedMessage, nameof(command.Slug));
			if (command.ImageFile == null || command.ImageFile.IsImage() == false)
				return new OperationResult(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
			string imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.ProductCategoryImageFolder);
			if (command.ImageFile == null || command.ImageFile.IsImage() == false)
				if (string.IsNullOrEmpty(imageName))
					return new OperationResult(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
			_fileService.ResizeImage(imageName, FileDirectories.ProductCategoryImageFolder, 500);
			_fileService.ResizeImage(imageName, FileDirectories.ProductCategoryImageFolder, 100);

			var category = new ProductCategory(command.Title.Trim(), slug, imageName, command.ImageAlt, command.Parent);
			if (await _productCategoryRepository.CreateAsync(category)) return new(true);
			if (command.ImageFile == null)
			{
				_fileService.DeleteImage($"{FileDirectories.ProductCategoryImageDirectory}{imageName}");
				_fileService.DeleteImage($"{FileDirectories.ProductCategoryImageDirectory500}{imageName}");
				_fileService.DeleteImage($"{FileDirectories.ProductCategoryImageDirectory100}{imageName}");
			}
			return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
		}

		public async Task<OperationResult> EditAsync(EditProductCategory command)
		{
			var category = await _productCategoryRepository.GetByIdAsync(command.Id);
			if (await _productCategoryRepository.ExistByAsync(p => p.Title.Trim().ToLower() == command.Title.Trim().ToLower() && p.Id != command.Id))
				return new OperationResult(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));
			var slug = SlugUtility.GenerateSlug(command.Slug);
			if (await _productCategoryRepository.ExistByAsync(p => p.Slug == slug && p.Id != command.Id))
				return new OperationResult(false, ValidationMessages.DuplicatedMessage, nameof(command.Slug));
			if (command.ImageFile != null && command.ImageFile.IsImage() == false)
				return new OperationResult(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
			string imageName = category.ImageName;
			string oldImageName = category.ImageName;
			if (command.ImageFile != null)
			{
				imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.ProductCategoryImageFolder);
				if (command.ImageFile == null || command.ImageFile.IsImage() == false)
					if (string.IsNullOrEmpty(imageName))
						return new OperationResult(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
				_fileService.ResizeImage(imageName, FileDirectories.ProductCategoryImageFolder, 500);
				_fileService.ResizeImage(imageName, FileDirectories.ProductCategoryImageFolder, 100);
			}
			category.Edit(command.Title, slug, imageName, command.ImageAlt);
			if (await _productCategoryRepository.SaveAsync())
			{
				if (command.ImageFile != null)
				{
					_fileService.DeleteImage($"{FileDirectories.ProductCategoryImageDirectory}{oldImageName}");
					_fileService.DeleteImage($"{FileDirectories.ProductCategoryImageDirectory500}{oldImageName}");
					_fileService.DeleteImage($"{FileDirectories.ProductCategoryImageDirectory100}{oldImageName}");
				}
				return new(true);
			}
			else
			{
				if (command.ImageFile != null)
				{
					_fileService.DeleteImage($"{FileDirectories.ProductCategoryImageDirectory}{imageName}");
					_fileService.DeleteImage($"{FileDirectories.ProductCategoryImageDirectory500}{imageName}");
					_fileService.DeleteImage($"{FileDirectories.ProductCategoryImageDirectory100}{imageName}");
				}
				return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
			}
		}

		public async Task<EditProductCategory> GetForEditAsync(int id)
		{
			var category = await _productCategoryRepository.GetByIdAsync(id);
			return new()
			{
				ImageAlt = category.ImageAlt,	
				Id = id,
				ImageFile = null,
				ImageName = category.ImageName,
				Slug = category.Slug,
				Title = category.Title
			};
		}
	}
}

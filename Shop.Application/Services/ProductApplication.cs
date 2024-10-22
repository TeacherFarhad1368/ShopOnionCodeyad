using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Shared;
using Shared.Application;
using Shared.Application.Services;
using Shop.Application.Contract.ProductApplication.Command;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductCategoryAgg;
using Shop.Domain.ProductCategoryRelationAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{

	internal class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRelationRepository _productCategoryRelationRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;  
        private readonly IFileService _fileService;

        public ProductApplication(IProductRepository productRepository, IProductCategoryRelationRepository productCategoryRelationRepository,
            IProductCategoryRepository productCategoryRepository, IFileService fileService)
        {
            _productRepository = productRepository;
            _productCategoryRelationRepository = productCategoryRelationRepository;
            _productCategoryRepository = productCategoryRepository;
            _fileService = fileService;
        }

        public async Task<bool> ActivationChange(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            product.ActivationChange();
            return await _productRepository.SaveAsync();
        }

        public async Task<OperationResult> CreateAsync(CreateProduct command)
        {
            if (await _productRepository.ExistByAsync(p => p.Title.Trim().ToLower() == command.Title.Trim().ToLower()))
                return new OperationResult(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));
            var slug = SlugUtility.GenerateSlug(command.Slug);
            if (await _productRepository.ExistByAsync(p => p.Slug == slug))
                return new OperationResult(false, ValidationMessages.DuplicatedMessage, nameof(command.Slug));
            if(await _productCategoryRepository.CheckProductCategoriesExist(command.Categoryids) == false)
                return new OperationResult(false, "دسته بندی ها را انتخاب کنید " , nameof(command.Categoryids));
            if(command.ImageFile == null || command.ImageFile.IsImage() == false)
                return new OperationResult(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
            string imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.ProductImageFolder);
            if(command.ImageFile == null || command.ImageFile.IsImage() == false)
            if(string.IsNullOrEmpty(imageName))
                return new OperationResult(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
            _fileService.ResizeImage(imageName, FileDirectories.ProductImageFolder, 500);
            _fileService.ResizeImage(imageName, FileDirectories.ProductImageFolder, 100);

            Product product = new(command.Title.Trim(), slug, command.ShortDescription, command.Text, imageName, command.ImageAlt, command.Weight);
            if (await _productRepository.CreateAsync(product)) return new(true);
            if(command.ImageFile != null)
            {
                _fileService.DeleteImage($"{FileDirectories.ProductImageDirectory}{imageName}");
                _fileService.DeleteImage($"{FileDirectories.ProductImageDirectory500}{imageName}");
                _fileService.DeleteImage($"{FileDirectories.ProductImageDirectory100}{imageName}");
            }
            return new(false,ValidationMessages.SystemErrorMessage, nameof(command.Title)); 
        }
        
        public async Task<OperationResult> EditAsync(EditProduct command)
        {
            var product = await _productRepository.GetByIdAsync(command.Id);
			if (await _productRepository.ExistByAsync(p => p.Title.Trim().ToLower() == command.Title.Trim().ToLower() && p.Id != product.Id))
				return new OperationResult(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));
			var slug = SlugUtility.GenerateSlug(command.Slug);
			if (await _productRepository.ExistByAsync(p => p.Slug == slug && p.Id != product.Id))
				return new OperationResult(false, ValidationMessages.DuplicatedMessage, nameof(command.Slug));
			if (await _productCategoryRepository.CheckProductCategoriesExist(command.Categoryids) == false)
				return new OperationResult(false, "دسته بندی ها را انتخاب کنید ", nameof(command.Categoryids));
			if (command.ImageFile != null && command.ImageFile.IsImage() == false)
				return new OperationResult(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
            string imageName = product.ImageName;
            string oldImageName = product.ImageName;
			if (command.ImageFile != null)
			{
				 imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.ProductImageFolder);
				if (command.ImageFile == null || command.ImageFile.IsImage() == false)
					if (string.IsNullOrEmpty(imageName))
						return new OperationResult(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
				_fileService.ResizeImage(imageName, FileDirectories.ProductImageFolder, 500);
				_fileService.ResizeImage(imageName, FileDirectories.ProductImageFolder, 100);
			}
            product.Edit(command.Title, slug, command.ShortDescription, command.Text, imageName, command.ImageAlt, command.Weight);
            if(await _productRepository.SaveAsync())
            {
				if (command.ImageFile != null)
				{
					_fileService.DeleteImage($"{FileDirectories.ProductImageDirectory}{oldImageName}");
					_fileService.DeleteImage($"{FileDirectories.ProductImageDirectory500}{oldImageName}");
					_fileService.DeleteImage($"{FileDirectories.ProductImageDirectory100}{oldImageName}");
				}
                return new(true);
			}
            else
            {
				if (command.ImageFile != null)
				{
					_fileService.DeleteImage($"{FileDirectories.ProductImageDirectory}{imageName}");
					_fileService.DeleteImage($"{FileDirectories.ProductImageDirectory500}{imageName}");
					_fileService.DeleteImage($"{FileDirectories.ProductImageDirectory100}{imageName}");
				}
				return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
			}
		}

        public async Task<EditProduct> GetForEditAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var categoreis = _productCategoryRelationRepository.GetAllBy(c => c.ProductId == id);
            return new()
            {
                ImageAlt = product.ImageAlt,
                Categoryids = categoreis.Select(c=> c.ProductCategoryId).ToList(),  
                Id = id,
                ImageFile = null,
                ImageName = product.ImageName,  
                ShortDescription = product.ShortDescription,
                Slug = product.Slug,
                Text = product.Description,
                Title = product.Title,
                Weight = product.Weight
            };
        }
    }
}

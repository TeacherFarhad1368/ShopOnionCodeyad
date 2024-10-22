using Shared;
using Shared.Application;
using Shared.Application.Services;
using Shop.Application.Contract.ProductGalleryApplication.Command;
using Shop.Domain.ProductGalleryAgg;

namespace Shop.Application.Services
{
	internal class ProductGalleryApplication : IProductGalleryApplication
	{
        private readonly IProductGalleryRepository _productGalleryRepository;
        private readonly IFileService _fileService;

		public ProductGalleryApplication(IProductGalleryRepository productGalleryRepository, IFileService fileService)
		{
			_productGalleryRepository = productGalleryRepository;
			_fileService = fileService;
		}

		public async Task<OperationResult> CreateAsync(CreateProductGallery command)
		{
			string imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.ProductGalleryImageFolder);
			if (command.ImageFile == null || command.ImageFile.IsImage() == false)
				if (string.IsNullOrEmpty(imageName))
					return new OperationResult(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));

			_fileService.ResizeImage(imageName, FileDirectories.ProductGalleryImageFolder, 100);
            var gallery = new ProductGallery(command.ProductId, imageName, command.ImageAlt);
            if (await _productGalleryRepository.CreateAsync(gallery)) return new(true);
			_fileService.DeleteImage($"{FileDirectories.ProductGalleryImageDirectory}{imageName}");
			_fileService.DeleteImage($"{FileDirectories.ProductGalleryImageDirectory100}{imageName}");
            return new(false, ValidationMessages.SystemErrorMessage);
		}

		public async Task<bool> DeleteAsync(int id)
		{
            var gallery = await _productGalleryRepository.GetByIdAsync(id);
            string imageName = gallery.ImageName;
            if(_productGalleryRepository.Delete(gallery))
			{
				_fileService.DeleteImage($"{FileDirectories.ProductGalleryImageDirectory}{imageName}");
				_fileService.DeleteImage($"{FileDirectories.ProductGalleryImageDirectory100}{imageName}");
                return true;
			}
            return false;
		}
	}
}

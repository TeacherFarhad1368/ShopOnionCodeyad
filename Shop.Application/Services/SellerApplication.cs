using Shared;
using Shared.Application;
using Shared.Application.BaseCommands;
using Shared.Application.Services;
using Shared.Domain.Enum;
using Shop.Application.Contract.SellerApplication.Command;
using Shop.Domain.SellerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
	internal class SellerApplication : ISellerApplication
	{
		private readonly ISellerRepository _sellerRepository;
		private readonly IFileService _fileService;

		public SellerApplication(ISellerRepository sellerRepository, IFileService fileService)
		{
			_sellerRepository = sellerRepository;
			_fileService = fileService;
		}

		public async Task<bool> ChangeStatusAsync(int id, SellerStatus status)
		{
			var seller = await _sellerRepository.GetByIdAsync(id);	
			seller.ChangeStatus(status);
			return await _sellerRepository.SaveAsync();
		}

		public async Task<OperationResult> EditRequestSellerAsync(int userId, EditSellerRequest command)
		{
			var seller = await _sellerRepository.GetByIdAsync(command.Id);
			if (seller == null || seller.UserId != userId) return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Id));
			if (command.ImageFile != null && command.ImageFile.IsImage() == false)
				return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
			if (command.ImageAccept != null && command.ImageAccept.IsImage() == false)
				return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageAccept));
			string imageName = seller.ImageName;
			string oldImageName = seller.ImageName;
			string imageAccept = seller.ImageAccept;
			string oldImageAccept = seller.ImageAccept;
			if (command.ImageFile != null)
			{
				 imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.SellerImageFolder);
				if (string.IsNullOrEmpty(imageName))
					return new OperationResult(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
				_fileService.ResizeImage(imageName, FileDirectories.SellerImageFolder, 500);
				_fileService.ResizeImage(imageName, FileDirectories.SellerImageFolder, 100);
			}
			if(command.ImageAccept != null)
			{
				imageAccept = _fileService.UploadImage(command.ImageAccept, FileDirectories.SellerImageFolder);
				if (string.IsNullOrEmpty(imageAccept))
					return new OperationResult(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageAccept));
				_fileService.ResizeImage(imageAccept, FileDirectories.SellerImageFolder, 500);
				_fileService.ResizeImage(imageAccept, FileDirectories.SellerImageFolder, 100);
			}

			 seller.Edit(command.Title, command.StateId, command.CityId, command.Address, command.GoogleMapUrl, imageName, command.ImageAlt, command.WhatsApp, command.Telegram, command.Instagram, command.Phone1, command.Phone2, command.Email);
			if (command.ImageAccept != null) seller.EditImageAccept(imageAccept);
			if (await _sellerRepository.SaveAsync())
			{
				if(command.ImageAccept != null)
				{
					_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory}{oldImageAccept}");
					_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory500}{oldImageAccept}");
					_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory100}{oldImageAccept}");
				}
				if(command.ImageFile != null)
				{
					_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory}{oldImageName}");
					_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory500}{oldImageName}");
					_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory100}{oldImageName}");
				}
				return new(true);
			}
			if (command.ImageAccept != null)
			{
				_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory}{imageAccept}");
				_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory500}{imageAccept}");
				_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory100}{imageAccept}");
			}
			if (command.ImageFile != null)
			{
				_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory}{imageName}");
				_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory500}{imageName}");
				_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory100}{imageName}");
			}
			return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
		}

		public async Task<EditSellerRequest> GetRequsetFoeEditAsync(int id, int userId)
		{
			var seller = await _sellerRepository.GetByIdAsync(id);
			if (seller.UserId != userId || seller.Status != SellerStatus.درخواست_تایید_نشده) return null;
			return new()
			{
				Address = seller.Address,	
				ImageAccept = null,
				ImageAcceptName = seller.ImageAccept,
				ImageAlt = seller.ImageAlt,
				WhatsApp = seller.WhatsApp,
				CityId = seller.CityId,
				Email = seller.Email,
				GoogleMapUrl = seller.GoogleMapUrl,
				Id = seller.Id,
				ImageFile = null,
				ImageName = seller.ImageName,
				Instagram = seller.Instagram,
				Phone1 = seller.Phone1,
				Phone2 = seller.Phone2,
				StateId = seller.StateId,
				Telegram = seller.Telegram,
				Title = seller.Title
			};
		}

		public async Task<OperationResult> RequestSellerAsync(int userId, RequestSeller command)
		{
			if(command.ImageFile == null || command.ImageFile.IsImage() == false) 
				return new(false,ValidationMessages.ImageErrorMessage,nameof(command.ImageFile));
			if (command.ImageAccept == null || command.ImageAccept.IsImage() == false)
				return new(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageAccept));
			string imageName = _fileService.UploadImage(command.ImageFile, FileDirectories.SellerImageFolder);
			if (string.IsNullOrEmpty(imageName))
				return new OperationResult(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageFile));
			_fileService.ResizeImage(imageName, FileDirectories.SellerImageFolder, 500);
			_fileService.ResizeImage(imageName, FileDirectories.SellerImageFolder, 100);
			string imageAccept = _fileService.UploadImage(command.ImageAccept, FileDirectories.SellerImageFolder);
			if (string.IsNullOrEmpty(imageAccept))
				return new OperationResult(false, ValidationMessages.ImageErrorMessage, nameof(command.ImageAccept));
			_fileService.ResizeImage(imageAccept, FileDirectories.SellerImageFolder, 500);
			_fileService.ResizeImage(imageAccept, FileDirectories.SellerImageFolder, 100);

			Seller seller = new Seller(userId, command.Title, command.StateId, command.CityId, command.Address, command.GoogleMapUrl, imageAccept, imageName, command.ImageAlt, command.WhatsApp, command.Telegram, command.Instagram, command.Phone1, command.Phone2, command.Email);
			if (await _sellerRepository.CreateAsync(seller)) return new(true);
			_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory}{imageName}");
			_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory500}{imageName}");
			_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory100}{imageName}");
			_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory}{imageAccept}");
			_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory500}{imageAccept}");
			_fileService.DeleteImage($"{FileDirectories.SellerImageDirectory100}{imageAccept}");
			return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
		}
	}
}

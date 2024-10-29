using Microsoft.EntityFrameworkCore.Storage;
using Shared.Application;
using Shop.Application.Contract.SellerPackegaApplication.Command;
using Shop.Domain.SellerPackageAgg;
using Shop.Domain.SellerPackageFeatureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
    internal class SellerPackegaApplication : ISellerPackegaApplication
    {
        private readonly ISellerPackageRepository _sellerPackageRepository;
        private readonly ISellerPackageFeatureRepository _sellerPackageFeatureRepository;

        public SellerPackegaApplication(ISellerPackageRepository sellerPackageRepository, 
            ISellerPackageFeatureRepository sellerPackageFeatureRepository)
        {
            _sellerPackageRepository = sellerPackageRepository;
            _sellerPackageFeatureRepository = sellerPackageFeatureRepository;
        }

        public async Task<OperationResult> CreateSellerPackageAsync(CreateSellerPackage command)
        {
            if (await _sellerPackageRepository.ExistByAsync(c => c.Title.Trim().ToLower() == command.Title.ToLower().Trim()))
                return new OperationResult(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));
            if(command.Percent < 0 || command.Percent > 100) 
                return new(false,"درصد تخفیف باید ما بین 0 تا 100 باشد .",nameof(command.Percent));   
            SellerPackage sellerPackage = new SellerPackage(command.Title,command.Description,command.Price,command.Percent,command.Mounth);
            if (await _sellerPackageRepository.CreateAsync(sellerPackage)) return new(true);
            return new(false,ValidationMessages.SystemErrorMessage,nameof(command.Title));
        }

        public async Task<OperationResult> CreateSellerPackageFeatureAsync(CreateSellerPackageFeature command)
        {
            if (await _sellerPackageFeatureRepository.ExistByAsync(c => c.Title.Trim().ToLower() == command.Title.ToLower().Trim() && c.SellerPackageId == command.SellerPackageId))
                return new OperationResult(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));
            SellerPackageFeature feature = new SellerPackageFeature(command.SellerPackageId, command.Title, command.Description);
            if(await _sellerPackageFeatureRepository.CreateAsync(feature)) return new(true);
            return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
        }

        public async Task<bool> DeleteSellerPackageFeatureAsync(int id)
        {
            var feature = await _sellerPackageFeatureRepository.GetByIdAsync(id);
            return await _sellerPackageFeatureRepository.DeleteAsync(feature);  
        }

        public async Task<OperationResult> EditSellerPackageAsync(EditSellerPackage command)
        {
            var sellerPackage = await _sellerPackageRepository.GetByIdAsync(command.Id);
            if (await _sellerPackageRepository.ExistByAsync(c => c.Title.Trim().ToLower() == command.Title.ToLower().Trim() && c.Id != command.Id))
                return new OperationResult(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));
            if (command.Percent < 0 || command.Percent > 100)
                return new(false, "درصد تخفیف باید ما بین 0 تا 100 باشد .", nameof(command.Percent));
            sellerPackage.Edit(command.Title,command.Description,command.Price,command.Percent,command.Mounth);
            if(await _sellerPackageRepository.SaveAsync()) return new(true);
            return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
        }

        public async Task<EditSellerPackage> GetSellerPackageForEditAsync(int id)
        {
            var sellerPackage = await _sellerPackageRepository.GetByIdAsync(id);
            return new()
            {
                Description = sellerPackage.Description,
                Id = id,
                Mounth = sellerPackage.Mounth,
                Percent = sellerPackage.Percent,
                Price = sellerPackage.Price,
                Title = sellerPackage.Title
            };
        }
    }
}

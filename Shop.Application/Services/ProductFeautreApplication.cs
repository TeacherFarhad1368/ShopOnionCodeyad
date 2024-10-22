using Shared.Application;
using Shop.Application.Contract.ProductFeautreApplication.Command;
using Shop.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
	internal class ProductFeautreApplication : IProductFeautreApplication
	{
		private readonly IProductFeatureRepository _featureRepository;

		public ProductFeautreApplication(IProductFeatureRepository featureRepository)
		{
			_featureRepository = featureRepository;
		}

		public async Task<OperationResult> CreateAsync(CreateProductFeautre command)
		{
			ProductFeature productFeature = new(command.ProductId, command.Title, command.Value);
			if (await _featureRepository.CreateAsync(productFeature)) return new(true);
			return new(false,ValidationMessages.SystemErrorMessage);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var feature = await _featureRepository.GetByIdAsync(id);
			return _featureRepository.Delete(feature);
		}
	}
}

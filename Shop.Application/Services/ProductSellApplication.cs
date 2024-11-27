using Shared.Application;
using Shop.Application.Contract.ProductSellApplication.Command;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductSellAgg;
using Shop.Domain.SellerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
	internal class ProductSellApplication : IProductSellApplication
	{
		private readonly IProductRepository _productRepository;	
		private readonly IProductSellRepository _productSellRepository;
		private readonly ISellerRepository _sellerRepository;

		public ProductSellApplication(IProductRepository productRepository, IProductSellRepository productSellRepository, ISellerRepository sellerRepository)
		{
			_productRepository = productRepository;
			_productSellRepository = productSellRepository;
			_sellerRepository = sellerRepository;
		}

		public async Task<bool> ActivationChangeAsync(int id)
		{
			var sell = await _productSellRepository.GetByIdAsync(id);
			sell.ActivationChange();
			return await _productSellRepository.SaveAsync();
		}

		public async Task<OperationResult> CreateAsync(CreateProductSell command)
		{
			if(command.ProductId == 0)
                return new(false, ValidationMessages.RequiredMessage, nameof(command.ProductId));
            var sell = new ProductSell(command.ProductId, command.SellerId, command.Price, command.Unit, command.Weight);
			if (await _productSellRepository.CreateAsync(sell)) return new(true);
			return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Unit));
		}

		public Task<OperationResult> EditAsync(EditProductSell command)
		{
			throw new NotImplementedException();
		}

		public Task<EditProductSell> GetForEditAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}

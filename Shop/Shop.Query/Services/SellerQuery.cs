using Shop.Application.Contract.SellerApplication.Query;
using Shop.Domain.SellerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Services
{
    internal class SellerQuery : ISellerQuery
    {
        private readonly ISellerRepository _sellerRepository;

        public SellerQuery(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
        }

        public int GetSellerUserId(int sellerId)
        {
            var seller = _sellerRepository.GetById(sellerId);   
            return seller.UserId;   
        }
    }
}

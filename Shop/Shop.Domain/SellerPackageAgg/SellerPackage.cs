using Shared.Domain;
using Shop.Domain.SellerPackageFeatureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.SellerPackageAgg
{
	public class SellerPackage : BaseEntityCreate<int>
	{
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Price { get; private set; }
        public int Percent { get; private set; }
        public int Mounth { get; private set; }
        public List<SellerPackageFeature> SellerPackageFeatures { get; private set; }
        public SellerPackage()
        {
			SellerPackageFeatures = new();

		}
		public void Edit(string title, string description, int price, int percent, int mounth)
		{
			Title = title;
			Description = description;
			Price = price;
			Percent = percent;
			Mounth = mounth;
		}
		public SellerPackage(string title, string description, int price, int percent, int mounth)
		{
			Title = title;
			Description = description;
			Price = price;
			Percent = percent;
			Mounth = mounth;
		}
	}
}

using Shared.Domain;
using Shop.Domain.SellerPackageAgg;
namespace Shop.Domain.SellerPackageFeatureAgg
{
	public class SellerPackageFeature : BaseEntityCreateActive<int>
    {
        public int SellerPackageId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public SellerPackage SellerPackage { get; private set; }
        public SellerPackageFeature()
        {
            SellerPackage = new();
        }

        public SellerPackageFeature(int sellerPackageId, string title, string description)
        {
            SellerPackageId = sellerPackageId;
            Title = title;
            Description = description;
        }
    }
}
